namespace Domain.WaterMeters
{
    public interface IWaterMeterRepository
    {
        Task<IReadOnlyList<WaterMeter>> GetAllAsync();
        Task<WaterMeter?> GetByIdAsync(WaterMeterId id);
        Task<bool> ExistsAsync(WaterMeterId id);
        Task Add(WaterMeter customer);
        Task Update(WaterMeter customer);
        Task Delete(WaterMeter customer);
    }
}
