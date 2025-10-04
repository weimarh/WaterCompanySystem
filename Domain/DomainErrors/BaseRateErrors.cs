using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class BaseRateErrors
    {
        public static Error BadCreatingRateFormat => Error.Validation(
            code: "BaseRate.BadCrationDateFormat",
            description: "Bad format in Creation Date"
        );

        public static Error BadAmountFormat => Error.Validation(
            code: "BaseRate.BadAmountFormat",
            description: "Bad format in Amount Date"
        );

        public static Error BaseRateNotFound => Error.NotFound(
            code: "BaseRate.BaseRateNotFound",
            description: "Base rate not found"
        );
    }
}
