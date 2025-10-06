using Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Payment payment) => await _context.Payments.AddAsync(payment);

        public void Delete(Payment payment) => _context.Payments.Update(payment);

        public async Task<bool> ExistsAsync(PaymentId id) => await _context.Payments.AnyAsync(p => p.PaymentId == id);

        public async Task<IReadOnlyList<Payment>> GetAllAsync() => await _context.Payments.ToListAsync();

        public async Task<Payment?> GetByIdAsync(PaymentId id) => await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);

        public void Update(Payment payment) => _context.Update(payment);
    }
}
