using Domain.Customers;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.Invoices.Create
{
    public record CreateInvoiceCommand(
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
        ServiceAddress ServiceAddress) : IRequest<ErrorOr<Unit>>;
}
