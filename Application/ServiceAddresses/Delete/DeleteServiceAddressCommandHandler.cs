using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ServiceAddresses;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Delete
{
    public sealed class DeleteServiceAddressCommandHandler : IRequestHandler<DeleteServiceAddressCommand, ErrorOr<Unit>>
    {
        public readonly IServiceAddressRepository _serviceAddressRepository;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteServiceAddressCommandHandler(IUnitOfWork unitOfWork, IServiceAddressRepository serviceAddressRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteServiceAddressCommand command, CancellationToken cancellationToken)
        {
            if (await _serviceAddressRepository.GetByIdAsync(new ServiceAddressId(command.ServiceAddressId)) is not ServiceAddress serviceAddress)
                return ServiceAddressErrors.ServiceAddressNotFound;

            _serviceAddressRepository.Delete(serviceAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
