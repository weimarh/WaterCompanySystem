using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Customers.Delete
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.CustomerId))
                return CustomerErrors.BadIdFormat;

            if (await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId)) is not Customer customer)
                return CustomerErrors.CustomerNotFound;

            await _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
