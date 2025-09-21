using Application.Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById
{
    public record GetCustomerByIdQuery(string CustomerId) : IRequest<ErrorOr<CustomerResponse>>;
}
