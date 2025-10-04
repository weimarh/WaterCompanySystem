using Domain.Invoices;
using MediatR;

namespace Domain.Events
{
    public record ReadingDeletedEvent(InvoiceId InvoiceId) : IDomainEvent, INotification
    {
        public DateTime OcurredOn => DateTime.UtcNow;
    }
}
