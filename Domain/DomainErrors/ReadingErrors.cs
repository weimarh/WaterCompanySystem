using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class ReadingErrors
    {
        public static Error BadReadingValueFormat => Error.Validation(
            code: "Reading.BadReadingValueFormat",
            description: "Bad format in a reading value"
        );

        public static Error BadReadingDateFormat => Error.Validation(
            code: "Reading.BadReadingDateFormat",
            description: "Bad format in the reading date"
        );

        public static Error TotalAmountError => Error.Validation(
            code: "Reading.TotalAmountError",
            description: "Total amount error"
        );

        public static Error WaterMeterNotFound => Error.NotFound(
            code: "Reading.WaterMeterNotFound",
            description: "Water meter not found"
        );

        public static Error ServiceAddressNotFound => Error.NotFound(
            code: "Reading.ServiceAddressNotFound",
            description: "Service address not found"
        );

        public static Error CustomerNotFound => Error.NotFound(
            code: "Reading.CustomerNotFound",
            description: "Customer not found"
        );

        public static Error ReadingNotFound => Error.NotFound(
            code: "Reading.ReadingNotFound",
            description: "Reading not found"
        );

        public static Error NoBaseRatesFound => Error.NotFound(
            code: "Reading.NoBaseRatesFound",
            description: "No base rates found"
        );

        public static Error NoInvoiceFound => Error.NotFound(
            code: "Reading.NoInvoiceFound",
            description: "No invoice found"
        );

        public static Error NoRatesPerCubicMeterFound => Error.NotFound(
            code: "Reading.NoRatesPerCubicMeterFound",
            description: "No rates per cubic meter found"
        );
    }
}
