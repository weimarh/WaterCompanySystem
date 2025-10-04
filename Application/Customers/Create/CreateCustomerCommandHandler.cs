using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.CustomerId))
                return CustomerErrors.BadIdFormat;

            if (string.IsNullOrWhiteSpace(command.FirstName))
                return CustomerErrors.BadFirstNameFormat;

            if (string.IsNullOrWhiteSpace(command.LastName))
                return CustomerErrors.BadLastNameFormat;

            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return CustomerErrors.BadPhoneNumberFormat;

            var customer = new Customer
            (
                new CustomerId(command.CustomerId),
                command.FirstName,
                command.LastName,
                phoneNumber
            );

            await _customerRepository.Add(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
