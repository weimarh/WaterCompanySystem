using Domain.Customers;
using Domain.Enums;
using Domain.Primitives;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Domain.WaterMeters;

namespace Domain.Invoices
{
    public sealed class Invoice : AggregateRoot
    {
        private Invoice() { }

        public Invoice(int invoiceNumber, BillingPeriod billingPeriod, Money totalAmountDue, DateTime dueDate, Money fine, bool isPaid)
        {
            InvoiceNumber = invoiceNumber;
            BillingPeriod = billingPeriod;
            TotalAmountDue = totalAmountDue;
            DueDate = dueDate;
            Fine = fine;
            IsPaid = isPaid;
        }

        public Invoice(InvoiceId invoiceId, int invoiceNumber, BillingPeriod billingPeriod, Money totalAmountDue, DateTime dueDate, Money fine, bool isPaid)
        {
            InvoiceId = invoiceId;
            InvoiceNumber = invoiceNumber;
            BillingPeriod = billingPeriod;
            TotalAmountDue = totalAmountDue;
            DueDate = dueDate;
            Fine = fine;
            IsPaid = isPaid;
        }

        public InvoiceId InvoiceId { get; private set; } = null!;
        public int InvoiceNumber { get; private set; }
        public BillingPeriod BillingPeriod { get; private set; }
        public Money TotalAmountDue { get; private set; } = null!;
        public DateTime DueDate { get; private set; }
        public Money Fine { get; private set; } = null!;
        public bool IsPaid { get; set; }

        public CustomerId CustomerId { get; private set; } = null!;
        public Customer Customer { get; private set; } = null!;
        public WaterMeterId WaterMeterId { get; private set; } = null!;
        public WaterMeter WaterMeter { get; private set; } = null!;
        public ReadingId ReadingId { get; private set; } = null!;
        public Reading Reading { get; private set; } = null!;
        public ServiceAddressId ServiceAddressId { get; private set; } = null!;
        public ServiceAddress ServiceAddress { get; private set; } = null!;

        public static Invoice UpdateInvoice(int invoiceNumber, BillingPeriod billingPeriod, Money totalAmountDue, DateTime dueDate, Money fine, bool isPaid)
        {
            return new Invoice(invoiceNumber, billingPeriod, totalAmountDue, dueDate, fine, isPaid);
        }
    }
}
