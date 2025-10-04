using Application.Invoices.Update;
using Domain.Events;
using MediatR;

namespace Application.Payments.DomainEventHandlers
{
    internal class PaymentDeletedEventHandler : INotificationHandler<PaymentDeletedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(PaymentDeletedEvent notification, CancellationToken cancellationToken)
        {
            var command = new UpdateInvoiceCommand(
                notification.Invoice.InvoiceId,
                notification.Invoice.BillingPeriod,
                notification.Invoice.DueDate,
                false,
                notification.Invoice.TotalAmountDue,
                notification.Invoice.ReadingId,
                notification.Invoice.Reading,
                notification.Invoice.CustomerId,
                notification.Invoice.Customer,
                notification.Invoice.WaterMeterId,
                notification.Invoice.WaterMeter,
                notification.Invoice.ServiceAddressId,
                notification.Invoice.ServiceAddress);

            await _mediator.Send(command, cancellationToken);
        }
    }
}
