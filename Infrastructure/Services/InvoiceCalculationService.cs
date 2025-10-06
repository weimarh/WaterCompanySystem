using Domain.DomainErrors;
using Domain.Services;
using Domain.ValueObjects;
using ErrorOr;

namespace Infrastructure.Services
{
    public class InvoiceCalculationService : IInvoiceCalculationService
    {
        public ErrorOr<Money> CalculateAmount(string previousReading, string latestReading, Money baseRate, Money ratePerCubicMeter)
        {
            decimal previous = decimal.Parse(previousReading);
            decimal latest = decimal.Parse(latestReading);
            decimal rate = decimal.Parse(ratePerCubicMeter.Value);

            decimal comsuption = previous - latest;

            if (comsuption < 0)
                return InvoiceErrors.InvoiceDueAmount;


            else if (comsuption == 0)
                return baseRate;

            else 
                return Money.Create((comsuption * rate).ToString()) ?? null!;
        }
    }
}
