using ErrorOr;
using MediatR;

namespace Application.Readings.Create
{
    public record CreateReadingCommand(
        DateTime ReadingDate,
        string ReadingValue,
        Guid WaterMeterId) : IRequest<ErrorOr<Unit>>;
}
