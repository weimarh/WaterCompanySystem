using Domain.DomainErrors;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Delete
{
    public sealed class DeleteRatePerCubicMeterCommandHandler : IRequestHandler<DeleteRatePerCubicMeterCommand, ErrorOr<Unit>>
    {
        public readonly IRatePerCubicMeterRepository _ratePerCubicMeterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteRatePerCubicMeterCommandHandler(IUnitOfWork unitOfWork, IRatePerCubicMeterRepository ratePerCubicMeterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _ratePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteRatePerCubicMeterCommand command, CancellationToken cancellationToken)
        {
            if (await _ratePerCubicMeterRepository.GetByIdAsync(new RatePerCubicMeterId(command.RatePerCubicMeterId)) is not RatePerCubicMeter ratePerCubicMeter)
                return RatePerCubicMeterErrors.RatePerCubicMeterNotFound;

            await _ratePerCubicMeterRepository.Delete(ratePerCubicMeter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
