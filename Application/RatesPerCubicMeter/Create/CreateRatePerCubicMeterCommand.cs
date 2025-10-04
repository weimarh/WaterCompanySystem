using ErrorOr;
using MediatR;

namespace Application.RatesPerCubicMeter.Create
{
    public record CreateRatePerCubicMeterCommand(DateTime CreationDate, string Amount) : IRequest<ErrorOr<Unit>>;
}
