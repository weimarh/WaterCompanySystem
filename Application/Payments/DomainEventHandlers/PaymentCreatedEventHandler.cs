using Application.Invoices.Update;
using Domain.Events;
using MediatR;

namespace Application.Payments.DomainEventHandlers
{
    public class PaymentCreatedEventHandler : INotificationHandler<PaymenCreatedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(PaymenCreatedEvent notification, CancellationToken cancellationToken)
        {
            var update = new UpdateInvoiceCommand(
                notification.Invoice.InvoiceId,
                notification.Invoice.BillingPeriod,
                notification.Invoice.DueDate,
                true,
                notification.Invoice.TotalAmountDue,
                notification.Invoice.ReadingId,
                notification.Invoice.Reading,
                notification.Invoice.CustomerId,
                notification.Invoice.Customer,
                notification.Invoice.WaterMeterId,
                notification.Invoice.WaterMeter,
                notification.Invoice.ServiceAddressId,
                notification.Invoice.ServiceAddress);

            await _mediator.Send(update, cancellationToken);
        }
    }
}
