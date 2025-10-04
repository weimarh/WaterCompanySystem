using Application.Readings.Common;
using ErrorOr;
using MediatR;

namespace Application.Readings.GetAll
{
    public record GetAllReadingsQuery() : IRequest<ErrorOr<IReadOnlyList<ReadingResponse>>>;
}
