using ErrorOr;

namespace Domain.Services
{
    public interface IInvoiceNumberCalculationService
    {
        ErrorOr<int> CalculateInvoiceNumber(Guid InvoiceId);
    }
}
