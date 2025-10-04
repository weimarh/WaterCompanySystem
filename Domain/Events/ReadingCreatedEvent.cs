using Domain.Customers;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Domain.WaterMeters;
using MediatR;

namespace Domain.Events
{
    public record ReadingCreatedEvent(
        ReadingId ReadingId,
        Reading Reading,
        DateTime BillingPeriod,
        DateTime DueDate,
        Money TotalAmountDue,
        CustomerId CustomerId,
        Customer Customer,
        WaterMeterId WaterMeterId,
        WaterMeter WaterMeter,
        ServiceAddressId ServiceAddressId,
        ServiceAddress ServiceAddress) : IDomainEvent, INotification
    {
        public DateTime OcurredOn => DateTime.UtcNow;
    }
}
