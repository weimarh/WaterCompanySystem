using Domain.DomainErrors;
using Domain.Invoices;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Invoices.Delete
{
    public sealed class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, ErrorOr<Unit>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteInvoiceCommand command, CancellationToken cancellationToken)
        {
            if (await _invoiceRepository.GetByIdAsync(new InvoiceId(command.InvoiceId)) is not Invoice invoice)
                return InvoiceErrors.InvoiceNotFound;

            _invoiceRepository.Delete(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
