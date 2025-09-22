using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Delete
{
    public record DeleteWaterMeterCommand(Guid WaterMeterId) : IRequest<ErrorOr<Unit>>;
}
