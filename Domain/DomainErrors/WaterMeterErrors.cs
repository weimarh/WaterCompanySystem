using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class WaterMeterErrors
    {
        public static Error BadModelFormat => Error.Validation(
            code: "WaterMeter.BadModelFormat",
            description: "Bad model format"
        );

        public static Error BadInstallationDateFormat => Error.Validation(
            code: "WaterMeter.BadInstallationDateFormat",
            description: "Bad Isntallation date format"
        );

        public static Error WaterMeterNotFound => Error.Validation(
            code: "WaterMeter.WaterMeterNotFound",
            description: "Water meter not found"
        );

        public static Error CustomerNotFound => Error.Validation(
            code: "WaterMeter.CustomerNotFound",
            description: "Customer not found"
        );

        public static Error ServiceAddressNotFound => Error.Validation(
            code: "WaterMeter.ServiceAddressNotFound",
            description: "Service address not found"
        );
    }
}
