using Domain.WaterMeters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.WaterMeters
{
    public class WaterMeterRepository : IWaterMeterRepository
    {
        private readonly ApplicationDbContext _context;

        public WaterMeterRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(WaterMeter waterMeter) => await _context.WaterMeters.AddAsync(waterMeter);

        public void Delete(WaterMeter waterMeter) => _context.WaterMeters.Remove(waterMeter);

        public async Task<bool> ExistsAsync(WaterMeterId id) => await _context.WaterMeters.AnyAsync(w => w.WaterMeterId == id);

        public async Task<IReadOnlyList<WaterMeter>> GetAllAsync() => await _context.WaterMeters.ToListAsync();

        public async Task<WaterMeter?> GetByIdAsync(WaterMeterId id) => await _context.WaterMeters.FirstOrDefaultAsync(w => w.WaterMeterId == id);

        public void Update(WaterMeter waterMeter) => _context.WaterMeters.Update(waterMeter);
    }
}
