using Domain.Customers;
using Domain.Invoices;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Domain.WaterMeters;
using MediatR;

namespace Domain.Events
{
    public record ReadingUpdatedEvent(
        Invoice Invoice,
        DateTime BillingPeriod,
        DateTime DueDate,
        bool IsPaid,
        Money TotalAmountDue,
        ReadingId ReadingId,
        Reading Reading,
        CustomerId CustomerId,
        Customer Customer,
        WaterMeterId WaterMeterId,
        WaterMeter WaterMeter,
        ServiceAddressId ServiceAddressId,
        ServiceAddress ServiceAddress) : IDomainEvent, INotification
    {
        public DateTime OcurredOn => throw new NotImplementedException();
    }
}
