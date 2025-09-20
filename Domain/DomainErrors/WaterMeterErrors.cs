using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class WaterMeterErrors
    {
        public static Error BadModelFormat => Error.Validation(
            code: "WaterMeter.BadModelFormat",
            description: "Bad model format"
        );

        public static Error WaterMeterNotFound => Error.Validation(
            code: "WaterMeter.WaterMeterNotFound",
            description: "Water meter not found"
        );
    }
}
