using ErrorOr;
using MediatR;

namespace Application.Readings.Update
{
    public record UpdateReadingCommand(
        Guid ReadingId,
        DateTime ReadingDate,
        string ReadingValue,
        Guid WaterMeterId) : IRequest<ErrorOr<Unit>>;
}
