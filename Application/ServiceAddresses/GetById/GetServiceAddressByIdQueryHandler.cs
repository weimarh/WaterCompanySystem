using Application.ServiceAddresses.Common;
using Domain.DomainErrors;
using Domain.ServiceAddresses;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.GetById
{
    public sealed class GetServiceAddressByIdQueryHandler : IRequestHandler<GetServiceAddressByIdQuery, ErrorOr<ServiceAddressResponse>>
    {
        public readonly IServiceAddressRepository _serviceAddressRepository;

        public GetServiceAddressByIdQueryHandler(IServiceAddressRepository serviceAddressRepository)
        {
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<ServiceAddressResponse>> Handle(GetServiceAddressByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _serviceAddressRepository.GetByIdAsync(new ServiceAddressId(query.ServiceAddressId)) is not ServiceAddress serviceAddress)
                return ServiceAddressErrors.ServiceAddressNotFound;

            return new ServiceAddressResponse
            (
                serviceAddress.ServiceAddressId.Value,
                serviceAddress.StreetName,
                serviceAddress.HouseNumber.Value,
                serviceAddress.RatePlan.ToString()
            );
        }
    }
}
