using Domain.WaterMeters;

namespace Domain.Readings
{
    public interface IReadingRepository
    {
        Task<IReadOnlyList<Reading>> GetAllAsync();
        Task<IReadOnlyList<Reading>> GetAllByWaterMeterIdAsync(WaterMeterId waterMeterId);
        Task<Reading?> GetByIdAsync(ReadingId id);
        Task<bool> ExistsAsync(ReadingId id);
        Task Add(Reading reading);
        void Update(Reading reading);
        void Delete(Reading reading);
    }
}
