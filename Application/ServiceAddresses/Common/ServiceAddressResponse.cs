namespace Application.ServiceAddresses.Common
{
    public record ServiceAddressResponse(
        Guid ServiceAddressId,
        string StreetName,
        string HouseNumber,
        string RatePlan);
}
