using Domain.DomainErrors;
using Domain.Events;
using Domain.Invoices;
using Domain.Payments;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Payments.Delete
{
    public sealed class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, ErrorOr<Unit>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public DeletePaymentCommandHandler(IUnitOfWork unitOfWork, IPaymentRepository paymentRepository, IPublisher publisher, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
        {
            if (await _paymentRepository.GetByIdAsync(new PaymentId(command.PaymentId)) is not Payment payment)
                return PaymentErrors.PaymentNotFound;

            if (await _invoiceRepository.GetByInvoiceNumberAsync(payment.InvoiceNumber) is not Invoice invoice)
                return PaymentErrors.InvoiceNotFound;

            _paymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new PaymentDeletedEvent(invoice));
            return Unit.Value;
        }
    }
}
