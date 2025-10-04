using Application.Invoices.Common;
using ErrorOr;
using MediatR;

namespace Application.Invoices.GetAll
{
    public record GetAllInvoicesQuery() : IRequest<ErrorOr<IReadOnlyList<InvoiceResponse>>>;
}
