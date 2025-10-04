using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class PaymentErrors
    {
        public static Error BadAmountFormat => Error.Validation(
            code: "Payment.BadAmountFormat",
            description: "Bad format in the amount"
        );

        public static Error PaymentNotFound => Error.NotFound(
            code: "Payment.PaymentNotFound",
            description: "Payment not found"
        );

        public static Error InvoiceNotFound => Error.NotFound(
            code: "Payment.InvoiceNotFound",
            description: "Invoice not found"
        );

        public static Error CustomerNotFound => Error.NotFound(
            code: "Payment.CustomerNotFound",
            description: "Customer not found"
        );

        public static Error WaterMeterNotFound => Error.NotFound(
            code: "Payment.WaterMeterNotFound",
            description: "Water meter not found"
        );
    }
}
