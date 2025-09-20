using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class CustomerErrors
    {
        public static Error BadIdFormat => Error.Validation(
            code: "Customer.BadIdFormat",
            description: "Bad format in ID"
        );

        public static Error BadFirstNameFormat => Error.Validation(
            code: "Customer.BadFirstNameFormat",
            description: "Bad format in first name"
        );

        public static Error BadLastNameFormat => Error.Validation(
            code: "Customer.BadLastNameFormat",
            description: "Bad format in last name"
        );

        public static Error BadPhoneNumberFormat => Error.Validation(
            code: "Customer.BadPhoneNumberFormat",
            description: "Bad format in phone number"
        );

        public static Error CustomerNotFound => Error.Validation(
            code: "Customer.CustomerNotFound",
            description: "Customer not found"
        );
    }
}
