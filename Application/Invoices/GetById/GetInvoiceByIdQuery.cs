using Application.Invoices.Common;
using ErrorOr;
using MediatR;

namespace Application.Invoices.GetById
{
    public record GetInvoiceByIdQuery(Guid InvoiceId) : IRequest<ErrorOr<InvoiceResponse>>;
}
