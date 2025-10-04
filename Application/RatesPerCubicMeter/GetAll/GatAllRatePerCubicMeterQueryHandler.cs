using Application.RatesPerCubicMeter.Common;
using Domain.RatesPerCubicMeter;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.GetAll
{
    public sealed class GatAllRatePerCubicMeterQueryHandler : IRequestHandler<GatAllRatePerCubicMeterQuery, ErrorOr<IReadOnlyList<RatePerCubicMeterResponse>>>
    {
        private readonly IRatePerCubicMeterRepository _ratePerCubicMeterRepository;

        public GatAllRatePerCubicMeterQueryHandler(IRatePerCubicMeterRepository ratePerCubicMeterRepository)
        {
            _ratePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<RatePerCubicMeterResponse>>> Handle(GatAllRatePerCubicMeterQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<RatePerCubicMeter> ratePerCubicMeters = await _ratePerCubicMeterRepository.GetAllAsync();

            return ratePerCubicMeters.Select(rate => new RatePerCubicMeterResponse(
                rate.RatePerCubicMeterId.Value,
                rate.CreationDate,
                rate.Amount.Value
            )).ToList();
        }
    }
}
