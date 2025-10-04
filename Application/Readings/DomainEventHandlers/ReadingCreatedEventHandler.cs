using Application.Invoices.Create;
using Domain.Events;
using MediatR;

namespace Application.Readings.DomainEventHandlers
{
    public class ReadingCreatedEventHandler : INotificationHandler<ReadingCreatedEvent>
    {
        private readonly IMediator _mediator;

        public ReadingCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(ReadingCreatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new CreateInvoiceCommand(
                notification.ReadingId,
                notification.Reading,
                notification.BillingPeriod,
                notification.DueDate,
                notification.TotalAmountDue,
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
