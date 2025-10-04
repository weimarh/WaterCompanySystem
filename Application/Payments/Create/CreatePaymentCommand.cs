using ErrorOr;
using MediatR;

namespace Application.Payments.Create
{
    public record CreatePaymentCommand(int InvoiceNumber, int PaymentMethod) : IRequest<ErrorOr<Unit>>;
}
