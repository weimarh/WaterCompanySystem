using Domain.BaseRates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.BaseRates
{
    public class BaseRateRepository : IBaseRateRepository
    {
        private readonly ApplicationDbContext _context;

        public BaseRateRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(BaseRate baseRate) => await _context.BaseRates.AddAsync(baseRate);

        public void Delete(BaseRate baseRate) => _context.BaseRates.Remove(baseRate);

        public async Task<bool> ExistsAsync(BaseRateId id) => await _context.BaseRates.AnyAsync(b => b.BaseRateId == id);

        public async Task<IReadOnlyList<BaseRate>> GetAllAsync() => await _context.BaseRates.ToListAsync();

        public async Task<BaseRate?> GetByIdAsync(BaseRateId id) => await _context.BaseRates.SingleOrDefaultAsync(b => b.BaseRateId == id);

        public void Update(BaseRate baseRate) => _context.BaseRates.Update(baseRate);
    }
}
