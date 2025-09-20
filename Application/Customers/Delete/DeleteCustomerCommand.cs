using ErrorOr;
using MediatR;

namespace Application.Customers.Delete
{
    public record DeleteCustomerCommand(string CustomerId) : IRequest<ErrorOr<Unit>>;
}
