namespace Application.WaterMeters.Common
{
    public record WaterMeterResponse
    (
        Guid WaterMeterId,
        string Model,
        DateTime InstallationDate,
        string ServiceAddress,
        string CustomerName
    );
}
