using Domain.Customers;
using Domain.Invoices;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.Invoices.Update
{
    public record UpdateInvoiceCommand(
        Invoice Invoice,
        DateTime BillingPeriod,
        DateTime DueDate,
        bool IsPaid,
        Money TotalAmountDue,
        ReadingId ReadingId,
        Reading Reading,
        CustomerId CustomerId,
        Customer Customer,
        WaterMeterId WaterMeterId,
        WaterMeter WaterMeter,
        ServiceAddressId ServiceAddressId,
        ServiceAddress ServiceAddress) : IRequest<ErrorOr<Unit>>;
}
