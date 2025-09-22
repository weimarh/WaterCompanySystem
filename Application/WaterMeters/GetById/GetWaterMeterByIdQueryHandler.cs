using Application.WaterMeters.Common;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.GetById
{
    public sealed class GetWaterMeterByIdQueryHandler : IRequestHandler<GetWaterMeterByIdQuery, ErrorOr<WaterMeterResponse>>
    {
        public readonly IWaterMeterRepository _waterMeterRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IServiceAddressRepository _serviceAddressRepository;

        public GetWaterMeterByIdQueryHandler(IServiceAddressRepository serviceAddressRepository, ICustomerRepository customerRepository, IWaterMeterRepository waterMeterRepository)
        {
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
        }

        public async Task<ErrorOr<WaterMeterResponse>> Handle(GetWaterMeterByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _waterMeterRepository.GetByIdAsync(new WaterMeterId(request.WaterMeterId)) is not WaterMeter waterMeter)
                return WaterMeterErrors.WaterMeterNotFound;

            var customer = await _customerRepository.GetByIdAsync(waterMeter.CustomerId);
            var serviceAddress = await _serviceAddressRepository.GetByIdAsync(waterMeter.ServiceAddressId);

            return new WaterMeterResponse(
                waterMeter.WaterMeterId.Value,
                waterMeter.Model,
                waterMeter.InstallationDate,
                serviceAddress?.StreetName ?? string.Empty + " " + serviceAddress?.HouseNumber ?? string.Empty,
                customer?.FirstName ?? string.Empty + " " + customer?.LastName ?? string.Empty
            );
        }
    }
}
