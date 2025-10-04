namespace Application.Invoices.Common
{
    public record InvoiceResponse(
        Guid InvoiceId,
        int InvoiceNumber,
        string BillingPeriod,
        string TotalAmountDue,
        DateTime DueDate,
        bool IsPaid,
        string CustomerName,
        string ServiceAddress);
}
