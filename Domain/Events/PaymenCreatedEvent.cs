using Domain.Invoices;
using Domain.Payments;
using MediatR;

namespace Domain.Events
{
    public record PaymenCreatedEvent(Invoice Invoice) : IDomainEvent, INotification
    {
        public DateTime OcurredOn => DateTime.Now;
    }
}
