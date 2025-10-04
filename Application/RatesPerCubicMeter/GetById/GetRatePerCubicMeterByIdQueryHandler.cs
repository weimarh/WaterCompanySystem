using Application.RatesPerCubicMeter.Common;
using Domain.DomainErrors;
using Domain.RatesPerCubicMeter;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.GetById
{
    public sealed class GetRatePerCubicMeterByIdQueryHandler : IRequestHandler<GetRatePerCubicMeterByIdQuery, ErrorOr<RatePerCubicMeterResponse>>
    {
        public readonly IRatePerCubicMeterRepository _RatePerCubicMeterRepository;

        public GetRatePerCubicMeterByIdQueryHandler(IRatePerCubicMeterRepository ratePerCubicMeterRepository)
        {
            _RatePerCubicMeterRepository = ratePerCubicMeterRepository ?? throw new ArgumentNullException(nameof(ratePerCubicMeterRepository));
        }

        public async Task<ErrorOr<RatePerCubicMeterResponse>> Handle(GetRatePerCubicMeterByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _RatePerCubicMeterRepository.GetByIdAsync(new RatePerCubicMeterId(query.RatePerCubicMeterId)) is not RatePerCubicMeter ratePerCubicMeter)
                return RatePerCubicMeterErrors.RatePerCubicMeterNotFound;

            return new RatePerCubicMeterResponse(ratePerCubicMeter.RatePerCubicMeterId.Value,
                ratePerCubicMeter.CreationDate,
                ratePerCubicMeter.Amount.Value);
        }
    }
}
