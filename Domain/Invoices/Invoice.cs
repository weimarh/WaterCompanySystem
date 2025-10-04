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

        public Invoice(InvoiceId invoiceId,
                       int invoiceNumber,
                       DateTime billingPeriod,
                       Money totalAmountDue,
                       DateTime dueDate,
                       bool isPaid,
                       CustomerId customerId,
                       Customer customer,
                       WaterMeterId waterMeterId,
                       WaterMeter waterMeter,
                       ReadingId readingId,
                       Reading reading,
                       ServiceAddressId serviceAddressId,
                       ServiceAddress serviceAddress)
        {
            InvoiceId = invoiceId;
            InvoiceNumber = invoiceNumber;
            BillingPeriod = billingPeriod;
            TotalAmountDue = totalAmountDue;
            DueDate = dueDate;
            IsPaid = isPaid;
            CustomerId = customerId;
            Customer = customer;
            WaterMeterId = waterMeterId;
            WaterMeter = waterMeter;
            ReadingId = readingId;
            Reading = reading;
            ServiceAddressId = serviceAddressId;
            ServiceAddress = serviceAddress;
        }

        public InvoiceId InvoiceId { get; private set; } = null!;
        public int InvoiceNumber { get; private set; }
        public DateTime BillingPeriod { get; private set; }
        public Money TotalAmountDue { get; private set; } = null!;
        public DateTime DueDate { get; private set; }
        public bool IsPaid { get; set; }
        public CustomerId CustomerId { get; private set; } = null!;
        public Customer Customer { get; private set; } = null!;
        public WaterMeterId WaterMeterId { get; private set; } = null!;
        public WaterMeter WaterMeter { get; private set; } = null!;
        public ReadingId ReadingId { get; private set; } = null!;
        public Reading Reading { get; private set; } = null!;
        public ServiceAddressId ServiceAddressId { get; private set; } = null!;
        public ServiceAddress ServiceAddress { get; private set; } = null!;

        public static Invoice UpdateInvoice(InvoiceId invoiceId,
                       int invoiceNumber,
                       DateTime billingPeriod,
                       Money totalAmountDue,
                       DateTime dueDate,
                       bool isPaid,
                       CustomerId customerId,
                       Customer customer,
                       WaterMeterId waterMeterId,
                       WaterMeter waterMeter,
                       ReadingId readingId,
                       Reading reading,
                       ServiceAddressId serviceAddressId,
                       ServiceAddress serviceAddress)
        {
            return new Invoice(
                invoiceId,
                invoiceNumber,
                billingPeriod,
                totalAmountDue,
                dueDate,
                isPaid,
                customerId,
                customer,
                waterMeterId,
                waterMeter,
                readingId,
                reading,
                serviceAddressId,
                serviceAddress);
        }
    }
}
