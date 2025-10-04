using Domain.Readings;

namespace Domain.Invoices
{
    public interface IInvoiceRepository
    {
        Task<IReadOnlyList<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(InvoiceId id);
        Task<Invoice?> GetByInvoiceNumberAsync(int invoiceNumber);
        Task<Invoice?> GetByReadingIdAsync(ReadingId id);
        Task<bool> ExistsAsync(InvoiceId id);
        Task Add(Invoice invoice);
        Task Update(Invoice invoice);
        Task Delete(Invoice invoice);
    }
}
