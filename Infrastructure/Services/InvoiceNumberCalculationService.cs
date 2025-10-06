using Domain.Services;
using ErrorOr;

namespace Infrastructure.Services
{
    public class InvoiceNumberCalculationService : IInvoiceNumberCalculationService
    {
        public ErrorOr<int> CalculateInvoiceNumber(Guid InvoiceId)
        {
            int intValue = InvoiceId.GetHashCode();

            return intValue == int.MinValue ? int.MaxValue : Math.Abs(intValue);
        }
    }
}
