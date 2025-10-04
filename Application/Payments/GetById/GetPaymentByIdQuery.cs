using Application.Payments.Common;
using ErrorOr;
using MediatR;

namespace Application.Payments.GetById
{
    public record GetPaymentByIdQuery(Guid PaymentId) : IRequest<ErrorOr<PaymentResponse>>;
}
