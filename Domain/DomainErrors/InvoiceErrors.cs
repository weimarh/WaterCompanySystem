using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class InvoiceErrors
    {
        public static Error InvoiceNotFound => Error.Validation(
            code: "Invoice.InvoiceNotFound",
            description: "Invoice not found"
        );
    }
}
