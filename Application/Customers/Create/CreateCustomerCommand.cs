using ErrorOr;
using MediatR;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand
    (
        string CustomerId,
        string FirstName,
        string LastName,
        string PhoneNumber
    ) : IRequest<ErrorOr<Unit>>;
}
