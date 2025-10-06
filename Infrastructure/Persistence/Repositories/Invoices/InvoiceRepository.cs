using Domain.Invoices;
using Domain.Readings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Invoices
{
    internal class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Invoice invoice) => await _context.Invoices.AddAsync(invoice);

        public void Delete(Invoice invoice) => _context.Invoices.Remove(invoice);

        public Task<bool> ExistsAsync(InvoiceId id) => _context.Invoices.AnyAsync(i => i.InvoiceId == id);

        public async Task<IReadOnlyList<Invoice>> GetAllAsync() => await _context.Invoices.ToListAsync();

        public async Task<Invoice?> GetByIdAsync(InvoiceId id) => await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);

        public async Task<Invoice?> GetByInvoiceNumberAsync(int invoiceNumber) => await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);

        public async Task<Invoice?> GetByReadingIdAsync(ReadingId id) => await _context.Invoices.FirstOrDefaultAsync(i => i.ReadingId == id);

        public void Update(Invoice invoice) => _context?.Update(invoice);
    }
}
