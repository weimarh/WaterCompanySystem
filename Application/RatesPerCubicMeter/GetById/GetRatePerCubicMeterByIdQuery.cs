using Application.RatesPerCubicMeter.Common;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.GetById
{
    public record GetRatePerCubicMeterByIdQuery(Guid RatePerCubicMeterId) : IRequest<ErrorOr<RatePerCubicMeterResponse>>;
}
