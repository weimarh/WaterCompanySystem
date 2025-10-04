using Application.Invoices.Delete;
using Domain.Events;
using MediatR;

namespace Application.Readings.DomainEventHandlers
{
    public class ReadingDeletedEventHandler : INotificationHandler<ReadingDeletedEvent>
    {
        private readonly IMediator _mediator;

        public ReadingDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(ReadingDeletedEvent notification, CancellationToken cancellationToken)
        {
            var command = new DeleteInvoiceCommand(notification.InvoiceId.Value);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
