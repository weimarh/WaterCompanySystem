using Application.WaterMeters.Common;
using Domain.Customers;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.GetAll
{
    public sealed class GetAllWaterMetersQueryHandler : IRequestHandler<GetAllWaterMetersQuery, ErrorOr<IReadOnlyList<WaterMeterResponse>>>
    {
        public readonly IWaterMeterRepository _waterMeterRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IServiceAddressRepository _serviceAddressRepository;

        public GetAllWaterMetersQueryHandler(IWaterMeterRepository waterMeterRepository, ICustomerRepository customerRepository, IServiceAddressRepository serviceAddressRepository)
        {
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<WaterMeterResponse>>> Handle(GetAllWaterMetersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<WaterMeter> waterMeters = await _waterMeterRepository.GetAllAsync();

            var waterMeterResponseTasks = waterMeters.Select(async waterMeter =>
            {
                var serviceAddress = await _serviceAddressRepository.GetByIdAsync(waterMeter.ServiceAddressId);
                var customer = await _customerRepository.GetByIdAsync(waterMeter.CustomerId);

                return new WaterMeterResponse
                (
                    waterMeter.WaterMeterId.Value,
                    waterMeter.Model,
                    waterMeter.InstallationDate,
                    serviceAddress?.StreetName ?? string.Empty + " " + serviceAddress?.HouseNumber ?? string.Empty,
                    (customer?.FirstName ?? string.Empty) + " " + (customer?.LastName ?? string.Empty)
                );
            }).ToList();

            var waterMeterResponses = await Task.WhenAll(waterMeterResponseTasks);

            return waterMeterResponses.ToList();
        }
    }
}
