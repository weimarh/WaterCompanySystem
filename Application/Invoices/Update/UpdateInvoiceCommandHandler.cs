using Domain.Invoices;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Invoices.Update
{
    public sealed class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, ErrorOr<Unit>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoice = Invoice.UpdateInvoice(
                command.Invoice.InvoiceId,
                command.Invoice.InvoiceNumber,
                command.BillingPeriod,
                command.TotalAmountDue,
                command.DueDate,
                command.IsPaid,
                command.CustomerId,
                command.Customer,
                command.WaterMeterId,
                command.WaterMeter,
                command.ReadingId,
                command.Reading,
                command.ServiceAddressId,
                command.ServiceAddress);

            await _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
