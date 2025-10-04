using Application.Payments.Common;
using ErrorOr;
using MediatR;

namespace Application.Payments.GetAll
{
    public record GetAllPaymentsQuery() : IRequest<ErrorOr<IReadOnlyList<PaymentResponse>>>;
}
