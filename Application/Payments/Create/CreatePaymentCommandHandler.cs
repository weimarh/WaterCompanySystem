using Domain.Customers;
using Domain.DomainErrors;
using Domain.Enums;
using Domain.Events;
using Domain.Invoices;
using Domain.Payments;
using Domain.Primitives;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.Payments.Create
{
    public sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ErrorOr<Unit>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWaterMeterRepository _waterMeterRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentCommandHandler(
            IInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork,
            IPaymentRepository paymentRepository,
            ICustomerRepository customerRepository,
            IWaterMeterRepository waterMeterRepository,
            IPublisher publisher)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<ErrorOr<Unit>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            if (await _invoiceRepository.GetByInvoiceNumberAsync(command.InvoiceNumber) is not Invoice invoice)
                return PaymentErrors.InvoiceNotFound;

            if (await _customerRepository.GetByIdAsync(invoice.CustomerId) is not Customer customer)
                return PaymentErrors.CustomerNotFound;

            if (await _waterMeterRepository.GetByIdAsync(invoice.WaterMeterId) is not WaterMeter waterMeter)
                return PaymentErrors.WaterMeterNotFound;

            var payment = new Payment(
                new PaymentId(Guid.NewGuid()),
                invoice.BillingPeriod,
                DateTime.Now,
                invoice.TotalAmountDue,
                (PaymentMethod)command.PaymentMethod,
                command.InvoiceNumber,
                invoice.InvoiceId,
                invoice,
                customer.CustomerId,
                customer,
                waterMeter.WaterMeterId,
                waterMeter);

            await _paymentRepository.Add(payment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new PaymenCreatedEvent(
                invoice));

            return Unit.Value;
        }
    }
}
