using Domain.RatesPerCubicMeter;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.RatesPerCubicMeter
{
    public class RatePerCubicMeterRepository : IRatePerCubicMeterRepository
    {
        private readonly ApplicationDbContext _context;

        public RatePerCubicMeterRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(RatePerCubicMeter ratePerCubicMeter) => await _context.RatePerCubicMeters.AddAsync(ratePerCubicMeter);

        public void Delete(RatePerCubicMeter ratePerCubicMeter) => _context.RatePerCubicMeters.Remove(ratePerCubicMeter);

        public async Task<bool> ExistsAsync(RatePerCubicMeterId id) => await _context.RatePerCubicMeters.AnyAsync(r => r.RatePerCubicMeterId == id);

        public async Task<IReadOnlyList<RatePerCubicMeter>> GetAllAsync() => await _context.RatePerCubicMeters.ToListAsync();

        public async Task<RatePerCubicMeter?> GetByIdAsync(RatePerCubicMeterId id) => await _context.RatePerCubicMeters.FirstOrDefaultAsync(r => r.RatePerCubicMeterId==id);

        public void Update(RatePerCubicMeter ratePerCubicMeter) => _context.RatePerCubicMeters.Update(ratePerCubicMeter);
    }
}
