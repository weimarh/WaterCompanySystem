using Domain.Readings;
using Domain.WaterMeters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Readings
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadingRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Reading reading) => await _context.Readings.AddAsync(reading);

        public void Delete(Reading reading) => _context.Readings.Remove(reading);

        public async Task<bool> ExistsAsync(ReadingId id) => await _context.Readings.AnyAsync(r => r.ReadingId == id);

        public async Task<IReadOnlyList<Reading>> GetAllAsync() => await _context.Readings.ToListAsync();

        public async Task<IReadOnlyList<Reading>> GetAllByWaterMeterIdAsync(WaterMeterId waterMeterId)
        {
            var readings = await _context.Readings.ToListAsync();
            return readings.Where(r => r.WaterMeterId == waterMeterId).ToList();
        }

        public async Task<Reading?> GetByIdAsync(ReadingId id) => await _context.Readings.FirstOrDefaultAsync(r => r.ReadingId == id);

        public void Update(Reading reading) => _context.Readings.Update(reading);
    }
}
