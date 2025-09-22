using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Update
{
    public record UpdateWaterMeterCommand(
        Guid WaterMeterId,
        string Model,
        DateTime InstallationDate,
        Guid ServiceAddressId,
        string CustomerId) : IRequest<ErrorOr<Unit>>;
}
