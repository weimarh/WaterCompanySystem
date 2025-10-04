using Domain.DomainErrors;
using Domain.Invoices;
using Domain.Primitives;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.Invoices.Create
{
    public sealed class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ErrorOr<Unit>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceNumberCalculationService _invoiceNumberCalculationService;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository, IInvoiceNumberCalculationService invoiceNumberCalculationService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoiceNumberCalculationService = invoiceNumberCalculationService ?? throw new ArgumentNullException(nameof(invoiceNumberCalculationService));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoiceId = new InvoiceId(Guid.NewGuid());

            var invoiceNumber = _invoiceNumberCalculationService.CalculateInvoiceNumber(invoiceId.Value);
            if (invoiceNumber.IsError)
                return InvoiceErrors.InvoiceNumberError;

            var invoice = new Invoice(
                invoiceId,
                invoiceNumber.Value,
                command.BillingPeriod,
                command.TotalAmountDue,
                command.DueDate,
                false,
                command.CustomerId,
                command.Customer,
                command.WaterMeterId,
                command.WaterMeter,
                command.ReadingId,
                command.Reading,
                command.ServiceAddressId, 
                command.ServiceAddress);

            await _invoiceRepository.Add(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
