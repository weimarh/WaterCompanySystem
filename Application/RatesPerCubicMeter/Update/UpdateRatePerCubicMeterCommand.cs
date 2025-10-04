using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Update
{
    public record UpdateRatePerCubicMeterCommand(Guid RatePerCubicMeterId, DateTime CreationDate, string Amount) : IRequest<ErrorOr<Unit>>;
}
