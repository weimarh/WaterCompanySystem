using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class ReadingErrors
    {
        public static Error BadReadingValueFormat => Error.Validation(
            code: "Reading.BadReadingValueFormat",
            description: "Bad format in a reading value"
        );

        public static Error ReadingNotFound => Error.Validation(
            code: "Reading.ReadingNotFound",
            description: "Reading not found"
        );
    }
}
