using Domain.DomainErrors;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Create
{
    public sealed class CreateRatePerCubicMeterCommandHandler : IRequestHandler<CreateRatePerCubicMeterCommand, ErrorOr<Unit>>
    {
        public readonly IRatePerCubicMeterRepository _ratePerCubicMeterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateRatePerCubicMeterCommandHandler(IRatePerCubicMeterRepository ratePerCubicMeterRepository, IUnitOfWork unitOfWork)
        {
            _ratePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateRatePerCubicMeterCommand command, CancellationToken cancellationToken)
        {
            if (command.CreationDate == DateTime.MinValue)
                return RatePerCubicMeterErrors.BadCreatingRatePerCubicMeterDateFormat;

            if (Money.Create(command.Amount) is not Money amount)
                return RatePerCubicMeterErrors.BadAmountFormat;

            var rate = new RatePerCubicMeter(
                new RatePerCubicMeterId(Guid.NewGuid()),
                command.CreationDate,
                amount);

            await _ratePerCubicMeterRepository.Add(rate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
