using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class RatePerCubicMeterErrors
    {
        public static Error BadCreatingRatePerCubicMeterDateFormat => Error.Validation(
            code: "RatePerCubicMeter.BadCreatingRatePerCubicMeterDateFormat",
            description: "Bad format in Creation Date"
        );

        public static Error BadAmountFormat => Error.Validation(
            code: "RatePerCubicMeter.BadAmountFormat",
            description: "Bad format in Amount Date"
        );

        public static Error RatePerCubicMeterNotFound => Error.NotFound(
            code: "RatePerCubicMeter.RatePerCubicMeterNotFound",
            description: "RatePerCubicMeter not found"
        );
    }
}
