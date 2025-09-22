using Application.ServiceAddresses.Common;
using Domain.ServiceAddresses;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.GetAll
{
    public sealed class GetAllServiceAddressesQueryHandler : IRequestHandler<GetAllServiceAddressesQuery, ErrorOr<IReadOnlyList<ServiceAddressResponse>>>
    {
        public readonly IServiceAddressRepository _serviceAddressRepository;

        public GetAllServiceAddressesQueryHandler(IServiceAddressRepository serviceAddressRepository)
        {
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<ServiceAddressResponse>>> Handle(GetAllServiceAddressesQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<ServiceAddress> serviceAddresses = await _serviceAddressRepository.GetAllAsync();

            return serviceAddresses.Select(serviceAddress => new ServiceAddressResponse(
                serviceAddress.ServiceAddressId.Value,
                serviceAddress.StreetName,
                serviceAddress.HouseNumber.Value,
                serviceAddress.RatePlan.ToString()
            )).ToList();
        }
    }
}
