using Domain.Customers;
using Domain.Enums;
using Domain.Invoices;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.WaterMeters;

namespace Domain.Payments
{
    public sealed class Payment : AggregateRoot
    {
        private Payment() { }

        public Payment(PaymentId paymentId, DateTime billingPeriod, DateTime paymentDate, Money amount, PaymentMethod paymentMethod, int invoiceNumber, InvoiceId invoiceId, Invoice invoice, CustomerId customerId, Customer customer, WaterMeterId waterMeterId, WaterMeter waterMeter)
        {
            PaymentId = paymentId;

            PaymentDate = paymentDate;
            BillingPeriod = billingPeriod;
            Amount = amount;
            PaymentMethod = paymentMethod;
            InvoiceNumber = invoiceNumber;
            InvoiceId = invoiceId;
            Invoice = invoice;
            CustomerId = customerId;
            Customer = customer;
            WaterMeterId = waterMeterId;
            WaterMeter = waterMeter;
        }

        public PaymentId PaymentId { get; private set; } = null!;
        public DateTime BillingPeriod { get; private set; }
        public DateTime PaymentDate { get; set; }
        public Money Amount { get; private set; } = null!;
        public PaymentMethod PaymentMethod { get; private set; }
        public int InvoiceNumber { get; private set; }
        public InvoiceId InvoiceId { get; private set; } = null!;
        public Invoice Invoice { get; private set; } = null!;
        public CustomerId CustomerId { get; private set; } = null!;
        public Customer Customer { get; private set; } = null!;
        public WaterMeterId WaterMeterId { get; private set; } = null!;
        public WaterMeter WaterMeter { get; private set; } = null!;

        public static Payment UpdatePayment(PaymentId paymentId,
                                            DateTime billingPeriod,
                                            DateTime paymentDate,
                                            Money amount,
                                            PaymentMethod paymentMethod,
                                            int invoiceNumber,
                                            InvoiceId invoiceId,
                                            Invoice invoice,
                                            CustomerId customerId,
                                            Customer customer,
                                            WaterMeterId waterMeterId,
                                            WaterMeter waterMeter)
        {
            return new Payment(
                paymentId,
                billingPeriod,
                paymentDate,
                amount,
                paymentMethod,
                invoiceNumber,
                invoiceId,
                invoice,
                customerId,
                customer,
                waterMeterId,
                waterMeter);
        }
    }
}
