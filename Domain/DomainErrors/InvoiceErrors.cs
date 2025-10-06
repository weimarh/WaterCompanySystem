using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class InvoiceErrors
    {
        public static Error InvoiceNumberError => Error.Validation(
            code: "Invoice.InvoiceNumberError",
            description: "Error generating invoice number"
        );

        public static Error InvoiceDueAmount => Error.Validation(
            code: "Invoice.InvoiceDueAmount",
            description: "Error generating due amount"
        );

        public static Error InvoiceNotFound => Error.NotFound(
            code: "Invoice.InvoiceNotFound",
            description: "Invoice not found"
        );

        public static Error CustomerNotFound => Error.NotFound(
            code: "Invoice.CustomerNotFound",
            description: "Customer not found"
        );

        public static Error ServiceAddressNotFound => Error.NotFound(
            code: "Invoice.ServiceAddressNotFound",
            description: "Service Address not found"
        );
    }
}
