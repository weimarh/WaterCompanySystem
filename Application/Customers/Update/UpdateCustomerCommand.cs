using ErrorOr;
using MediatR;

namespace Application.Customers.Update
{
    public record UpdateCustomerCommand(
        string CustomerId,
        string FirstName,
        string LastName,
        string PhoneNumber) : IRequest<ErrorOr<Unit>>;
}
