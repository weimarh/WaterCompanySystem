using Domain.ValueObjects;
using ErrorOr;

namespace Domain.Services
{
    public interface IInvoiceCalculationService
    {
        ErrorOr<Money> CalculateAmount(string previousReading, string latestReading, Money baseRate, Money ratePerCubicMeter);
    }
}
