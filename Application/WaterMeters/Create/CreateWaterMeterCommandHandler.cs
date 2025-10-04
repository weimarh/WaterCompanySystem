using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Create
{
    public sealed class CreateWaterMeterCommandHandler : IRequestHandler<CreateWaterMeterCommand, ErrorOr<Unit>>
    {
        public readonly IWaterMeterRepository _waterMeterRepository;
        public readonly IServiceAddressRepository _serviceAddressRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateWaterMeterCommandHandler(IUnitOfWork unitOfWork, IWaterMeterRepository waterMeterRepository, ICustomerRepository customerRepository, IServiceAddressRepository serviceAddressRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateWaterMeterCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Model))
                return WaterMeterErrors.BadModelFormat;

            if (string.IsNullOrWhiteSpace(command.InstallationDate.ToString()))
                return WaterMeterErrors.BadInstallationDateFormat;

            if (await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId)) is not Customer customer)
                return WaterMeterErrors.CustomerNotFound;

            if (await _serviceAddressRepository.GetByIdAsync(new ServiceAddressId(Guid.Parse(command.ServiceAddressId))) is not ServiceAddress serviceAddress)
                return WaterMeterErrors.ServiceAddressNotFound;

            var waterMeter = new WaterMeter
            (
                new WaterMeterId(Guid.NewGuid()),
                command.Model,
                command.InstallationDate,
                new ServiceAddressId(Guid.Parse(command.ServiceAddressId)),
                serviceAddress,
                new CustomerId(command.CustomerId),
                customer
            );

            await _waterMeterRepository.Add(waterMeter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
