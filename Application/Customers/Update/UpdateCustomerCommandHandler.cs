using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Update
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (!await _customerRepository.ExistsAsync(new CustomerId(command.CustomerId)))
                return CustomerErrors.CustomerNotFound;

            if (string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length < 2 || command.FirstName.Length > 15)
                return CustomerErrors.BadFirstNameFormat;

            if (string.IsNullOrWhiteSpace(command.LastName) || command.LastName.Length < 2 || command.LastName.Length > 15)
                return CustomerErrors.BadLastNameFormat;

            if (string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length < 2 || command.FirstName.Length > 15)
                return CustomerErrors.BadFirstNameFormat;

            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return CustomerErrors.BadPhoneNumberFormat;

            Customer customer = Customer.UpdateCustomer(
                new CustomerId(command.CustomerId),
                command.FirstName,
                command.LastName,
                phoneNumber);

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
