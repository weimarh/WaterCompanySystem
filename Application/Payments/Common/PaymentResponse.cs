namespace Application.Payments.Common
{
    public record PaymentResponse(
        Guid PaymentId,
        string BillingPeriod,
        DateTime PaymentDate,
        string Amount,
        string PaymentMethod,
        int InvoiceNumber,
        string CustomerName);
}
