using Domain.Enums;
using Domain.Invoices;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Payments
{
    public sealed class Payment : AggregateRoot
    {
        private Payment() { }

        public Payment(DateTime paymentDate, Money amount, PaymentMethod paymentMethod, InvoiceId invoiceId, Invoice invoice)
        {
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
            InvoiceId = invoiceId;
            Invoice = invoice;
        }

        public Payment(PaymentId paymentId, DateTime paymentDate, Money amount, PaymentMethod paymentMethod, InvoiceId invoiceId, Invoice invoice)
        {
            PaymentId = paymentId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
            InvoiceId = invoiceId;
            Invoice = invoice;
        }

        public PaymentId PaymentId { get; private set; } = null!;
        public DateTime PaymentDate { get; set; }
        public Money Amount { get; private set; } = null!;
        public PaymentMethod PaymentMethod { get; private set; }
        public InvoiceId InvoiceId { get; private set; } = null!;
        public Invoice Invoice { get; private set; } = null!;

        public static Payment UpdatePayment(DateTime paymentDate, Money amount, PaymentMethod paymentMethod, InvoiceId invoiceId, Invoice invoice)
        {
            return new Payment(paymentDate, amount, paymentMethod, invoiceId, invoice);
        }
    }
}
