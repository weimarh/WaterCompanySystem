using Domain.DomainErrors;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Update
{
    public sealed class UpdateRatePerCubicMeterCommandHandler : IRequestHandler<UpdateRatePerCubicMeterCommand, ErrorOr<Unit>>
    {
        public readonly IRatePerCubicMeterRepository _ratePerCubicMeterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public UpdateRatePerCubicMeterCommandHandler(IUnitOfWork unitOfWork, IRatePerCubicMeterRepository ratePerCubicMeterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _ratePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateRatePerCubicMeterCommand command, CancellationToken cancellationToken)
        {
            if (!await _ratePerCubicMeterRepository.ExistsAsync(new RatePerCubicMeterId(command.RatePerCubicMeterId)))
                return RatePerCubicMeterErrors.RatePerCubicMeterNotFound;

            if (command.CreationDate == DateTime.MinValue)
                return RatePerCubicMeterErrors.BadCreatingRatePerCubicMeterDateFormat;

            if (Money.Create(command.Amount) is not Money amount)
                return RatePerCubicMeterErrors.BadAmountFormat;

            var rate = RatePerCubicMeter.UpdateRatePerCubicMeter(
                new RatePerCubicMeterId(command.RatePerCubicMeterId),
                command.CreationDate,
                amount);

            _ratePerCubicMeterRepository.Update(rate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
