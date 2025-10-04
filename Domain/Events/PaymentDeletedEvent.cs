using Domain.Invoices;
using MediatR;

namespace Domain.Events
{
    public record PaymentDeletedEvent(Invoice Invoice) : IDomainEvent, INotification
    {
        public DateTime OcurredOn => DateTime.Now;
    }
}
