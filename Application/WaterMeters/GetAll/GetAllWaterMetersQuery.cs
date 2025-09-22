using Application.WaterMeters.Common;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.GetAll
{
    public record GetAllWaterMetersQuery() : IRequest<ErrorOr<IReadOnlyList<WaterMeterResponse>>>;
}
