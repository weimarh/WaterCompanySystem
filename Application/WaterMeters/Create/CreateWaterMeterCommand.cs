using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Create
{
    public record CreateWaterMeterCommand
    (
        string Model,
        DateTime InstallationDate,
        string ServiceAddressId,
        string CustomerId
    ) : IRequest<ErrorOr<Unit>>;
}
