namespace Application.RatesPerCubicMeter.Common
{
    public record RatePerCubicMeterResponse(
        Guid RatePerCubicMeterId,
        DateTime CreationDate,
        string Amount);
}
