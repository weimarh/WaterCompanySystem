using ErrorOr;
using MediatR;

namespace Application.Payments.Delete
{
    public record DeletePaymentCommand(Guid PaymentId) : IRequest<ErrorOr<Unit>>;
}
