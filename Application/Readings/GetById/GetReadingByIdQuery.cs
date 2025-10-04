using Application.Readings.Common;
using ErrorOr;
using MediatR;

namespace Application.Readings.GetById
{
    public record GetReadingByIdQuery(Guid ReadingId) : IRequest<ErrorOr<ReadingResponse>>;
}
