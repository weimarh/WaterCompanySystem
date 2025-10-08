using Domain.DomainErrors;
using Domain.Enums;
using Domain.Primitives;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Update
{
    public sealed class UpdateServiceAddressCommandHandler : IRequestHandler<UpdateServiceAddressCommand, ErrorOr<Unit>>
    {
        public readonly IServiceAddressRepository _serviceAddressRepository;
        public readonly IUnitOfWork _unitOfWork;

        public UpdateServiceAddressCommandHandler(IUnitOfWork unitOfWork, IServiceAddressRepository serviceAddressRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateServiceAddressCommand command, CancellationToken cancellationToken)
        {
            if (!await _serviceAddressRepository.ExistsAsync(new ServiceAddressId(command.ServiceAddressId)))
                return ServiceAddressErrors.ServiceAddressNotFound;

            if (string.IsNullOrWhiteSpace(command.StreetName))
                return ServiceAddressErrors.BadStreetNameFormat;

            if (HouseNumber.Create(command.HouseNumber) is not HouseNumber houseNumber)
                return ServiceAddressErrors.BadHouseNumberFormat;

            if (!Enum.IsDefined(typeof(RatePlan), command.RatePlan))
                return ServiceAddressErrors.BadRatePlanFormat;

            ServiceAddress serviceAddress = ServiceAddress.UpdateServiceAddress(
                new ServiceAddressId(command.ServiceAddressId),
                command.StreetName,
                houseNumber,
                command.RatePlan);

            _serviceAddressRepository.Update(serviceAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
