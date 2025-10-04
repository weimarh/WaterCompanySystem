namespace Application.Readings.Common
{
    public record ReadingResponse(Guid ReadingId, DateTime ReadingDate, string ReadingValue, Guid waterMeterId);
}
