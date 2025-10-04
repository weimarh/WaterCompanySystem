using ErrorOr;
using MediatR;

namespace Application.Invoices.Delete
{
    public record DeleteInvoiceCommand(Guid InvoiceId) : IRequest<ErrorOr<Unit>>;
}
