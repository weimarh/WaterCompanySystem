using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Update
{
    public sealed class UpdateWaterMeterCommandHandler : IRequestHandler<UpdateWaterMeterCommand, ErrorOr<Unit>>
    {
        public readonly IWaterMeterRepository _waterMeterRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IServiceAddressRepository _serviceAddressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWaterMeterCommandHandler(IServiceAddressRepository serviceAddressRepository, ICustomerRepository customerRepository, IWaterMeterRepository waterMeterRepository, IUnitOfWork unitOfWork)
        {
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateWaterMeterCommand command, CancellationToken cancellationToken)
        {
            if (!await _waterMeterRepository.ExistsAsync(new WaterMeterId(command.WaterMeterId)))
                return WaterMeterErrors.WaterMeterNotFound;

            if (await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId)) is not Customer customer)
                return WaterMeterErrors.CustomerNotFound;

            if (await _serviceAddressRepository.GetByIdAsync(new ServiceAddressId(command.ServiceAddressId)) is not  ServiceAddress serviceAddress)
                return WaterMeterErrors.ServiceAddressNotFound;

            if (string.IsNullOrWhiteSpace(command.Model))
                return WaterMeterErrors.BadModelFormat;

            if (string.IsNullOrWhiteSpace(command.InstallationDate.ToString()))
                return WaterMeterErrors.BadInstallationDateFormat;

            WaterMeter waterMeter = WaterMeter.UpdateWaterMeter(
                new WaterMeterId(command.WaterMeterId),
                command.Model,
                command.InstallationDate,
                serviceAddress.ServiceAddressId,
                serviceAddress,
                customer.CustomerId,
                customer);

            await _waterMeterRepository.Update(waterMeter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
