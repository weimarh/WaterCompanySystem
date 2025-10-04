using Application.RatesPerCubicMeter.Common;
using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.GetAll
{
    public record GatAllRatePerCubicMeterQuery : IRequest<ErrorOr<IReadOnlyList<RatePerCubicMeterResponse>>>;
}
