using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Delete
{
    public record DeleteRatePerCubicMeterCommand(Guid RatePerCubicMeterId) : IRequest<ErrorOr<Unit>>;
}
