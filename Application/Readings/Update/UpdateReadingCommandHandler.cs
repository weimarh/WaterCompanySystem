using Domain.BaseRates;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Events;
using Domain.Invoices;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.Services;
using Domain.ValueObjects;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.Readings.Update
{
    public sealed class UpdateReadingCommandHandler : IRequestHandler<UpdateReadingCommand, ErrorOr<Unit>>
    {
        private readonly IReadingRepository _readingRepository;
        private readonly IWaterMeterRepository _waterMeterRepository;
        private readonly IServiceAddressRepository _serviceAddressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;
        private readonly IInvoiceCalculationService _invoiceCalculationService;
        private readonly IBaseRateRepository _baseRateRepository;
        private readonly IRatePerCubicMeterRepository _ratePerCubicMeterRepository;
        private readonly IDueDateCalculator _dueDateCalculator;
        private readonly IInvoiceRepository _invoiceRepository;

        public UpdateReadingCommandHandler(
            IUnitOfWork unitOfWork,
            IReadingRepository readingRepository,
            IWaterMeterRepository waterMeterRepository,
            IPublisher publisher,
            IInvoiceCalculationService invoiceCalculationService,
            IServiceAddressRepository serviceAddressRepository,
            ICustomerRepository customerRepository,
            IBaseRateRepository baseRateRepository,
            IRatePerCubicMeterRepository ratePerCubicMeterRepository,
            IDueDateCalculator dueDateCalculator,
            IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _readingRepository = readingRepository ?? throw new ArgumentNullException(nameof(readingRepository));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _invoiceCalculationService = invoiceCalculationService ?? throw new ArgumentNullException(nameof(invoiceCalculationService));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
            _ratePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
            _dueDateCalculator = dueDateCalculator ?? throw new ArgumentNullException(nameof(dueDateCalculator));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateReadingCommand command, CancellationToken cancellationToken)
        {
            if (!await _readingRepository.ExistsAsync(new ReadingId(command.ReadingId)))
                return ReadingErrors.ReadingNotFound;

            if (await _invoiceRepository.GetByReadingIdAsync(new ReadingId(command.ReadingId)) is not Invoice invoice)
                return ReadingErrors.NoInvoiceFound;

            if (ReadingValue.Create(command.ReadingValue) is not ReadingValue readingValue)
                return ReadingErrors.BadReadingValueFormat;

            if (command.ReadingDate == DateTime.MinValue)
                return ReadingErrors.BadReadingDateFormat;

            if (await _waterMeterRepository.GetByIdAsync(new WaterMeterId(command.WaterMeterId)) is not WaterMeter waterMeter)
                return ReadingErrors.WaterMeterNotFound;

            if (await _serviceAddressRepository.GetByIdAsync(waterMeter.ServiceAddressId) is not ServiceAddress serviceAddress)
                return ReadingErrors.ServiceAddressNotFound;

            if (await _customerRepository.GetByIdAsync(waterMeter.CustomerId) is not Customer customer)
                return ReadingErrors.CustomerNotFound;

            var currentBaseRates = await _baseRateRepository.GetAllAsync();
            if (currentBaseRates.Count == 0)
                return ReadingErrors.NoBaseRatesFound;

            var currentRatesPerCubicMeter = await _ratePerCubicMeterRepository.GetAllAsync();
            if (currentRatesPerCubicMeter.Count == 0)
                return ReadingErrors.NoRatesPerCubicMeterFound;

            var readingsByWaterMeterId = await _readingRepository.GetAllByWaterMeterIdAsync(waterMeter.WaterMeterId);

            string previusReading = string.Empty;

            if (readingsByWaterMeterId.Count == 0)
                previusReading = "0";
            else
                previusReading = readingsByWaterMeterId[readingsByWaterMeterId.Count - 1].ReadingValue.ToString();

            var totalAmountDue = _invoiceCalculationService.CalculateAmount(
                previusReading,
                readingValue.Value,
                currentBaseRates[currentBaseRates.Count - 1].Amount,
                currentRatesPerCubicMeter[currentRatesPerCubicMeter.Count - 1].Amount);

            if (totalAmountDue.IsError)
                return ReadingErrors.TotalAmountError;

            var dueDate = _dueDateCalculator.CalculateDueDate(command.ReadingDate);

            if (dueDate.IsError)
                return ReadingErrors.BadReadingDateFormat;

            Reading reading = Reading.UpdateReading(
                new ReadingId(command.ReadingId),
                command.ReadingDate,
                readingValue,
                waterMeter.WaterMeterId,
                waterMeter);

            _readingRepository.Update(reading);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new ReadingUpdatedEvent(
                invoice,
                command.ReadingDate,
                dueDate.Value,
                false,
                totalAmountDue.Value,
                reading.ReadingId,
                reading,
                customer.CustomerId,
                customer,
                waterMeter.WaterMeterId,
                waterMeter,
                serviceAddress.ServiceAddressId,
                serviceAddress
                ));

            return Unit.Value;
        }
    }
}
