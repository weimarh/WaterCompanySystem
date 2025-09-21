using Application.Customers.Common;
using Domain.Customers;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId)) is not Customer customer)
                return CustomerErrors.CustomerNotFound;

            return new CustomerResponse
            (
                customer.CustomerId.Value,
                customer.FirstName + " " + customer.LastName,
                customer.PhoneNumber.Value
            );
        }
    }
}
