using Application.Invoices.Update;
using Domain.Events;
using MediatR;

namespace Application.Readings.DomainEventHandlers
{
    public class ReadingUpdatedEventHandler : INotificationHandler<ReadingUpdatedEvent>
    {
        private readonly IMediator _mediator;

        public ReadingUpdatedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(ReadingUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new UpdateInvoiceCommand(
                notification.Invoice,
                notification.BillingPeriod,
                notification.DueDate,
                notification.IsPaid,
                notification.TotalAmountDue,
                notification.ReadingId,
                notification.Reading,
                notification.CustomerId,
                notification.Customer,
                notification.WaterMeterId,
                notification.WaterMeter,
                notification.ServiceAddressId,
                notification.ServiceAddress);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
