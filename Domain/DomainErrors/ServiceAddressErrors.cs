using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class ServiceAddressErrors
    {
        public static Error BadStreetNameFormat => Error.Validation(
            code: "ServiceAddress.BadStreetNameFormat",
            description: "Bad format in street name"
        );

        public static Error BadHouseNumberFormat => Error.Validation(
            code: "ServiceAddress.BadHouseNumberFormat",
            description: "Bad format in house number"
        );

        public static Error BadRatePlanFormat => Error.Validation(
            code: "ServiceAddress.BadRatePlanFormat",
            description: "Bad format in rate plan"
        );

        public static Error ServiceAddressNotFound => Error.Validation(
            code: "ServiceAddress.ServiceAddressNotFound",
            description: "Service Address not found"
        );
    }
}
