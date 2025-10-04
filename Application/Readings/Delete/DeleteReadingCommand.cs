using ErrorOr;
using MediatR;

namespace Application.Readings.Delete
{
    public record DeleteReadingCommand(Guid ReadingId) : IRequest<ErrorOr<Unit>>;
}
