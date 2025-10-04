using Domain.DomainErrors;
using Domain.Enums;
using Domain.Primitives;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Create
{
    public sealed class CreateServiceAddressCommandHandler : IRequestHandler<CreateServiceAddressCommand, ErrorOr<Unit>>
    {
        public readonly IServiceAddressRepository _serviceAddressRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateServiceAddressCommandHandler(IUnitOfWork unitOfWork, IServiceAddressRepository serviceAddressRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateServiceAddressCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.StreetName))
                return ServiceAddressErrors.BadStreetNameFormat;

            if (HouseNumber.Create(command.HouseNumber) is not HouseNumber houseNumber)
                return ServiceAddressErrors.BadHouseNumberFormat;

            if (!Enum.IsDefined(typeof(RatePlan), command.RatePlan))
                return ServiceAddressErrors.BadRatePlanFormat;

            var serviceAddress = new ServiceAddress
            (
                new ServiceAddressId(Guid.NewGuid()),
                command.StreetName,
                houseNumber,
                command.RatePlan
            );

            await _serviceAddressRepository.Add(serviceAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
