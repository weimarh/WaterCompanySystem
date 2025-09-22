using Application.WaterMeters.Common;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.GetById
{
    public record GetWaterMeterByIdQuery(Guid WaterMeterId) : IRequest<ErrorOr<WaterMeterResponse>>;
}
