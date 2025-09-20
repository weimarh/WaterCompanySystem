using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class PaymentErrors
    {
        public static Error BadAmountFormat => Error.Validation(
            code: "Payment.BadAmountFormat",
            description: "Bad format in the amount"
        );

        public static Error PaymentNotFound => Error.Validation(
            code: "Payment.PaymentNotFound",
            description: "Payment not found"
        );
    }
}
