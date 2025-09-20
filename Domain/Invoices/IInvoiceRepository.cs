namespace Domain.Invoices
{
    public interface IInvoiceRepository
    {
        Task<IReadOnlyList<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(InvoiceId id);
        Task<bool> ExistsAsync(InvoiceId id);
        Task Add(Invoice customer);
        Task Update(Invoice customer);
        Task Delete(Invoice customer);
    }
}
