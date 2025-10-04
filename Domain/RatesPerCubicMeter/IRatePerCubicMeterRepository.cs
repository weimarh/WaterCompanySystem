namespace Domain.RatesPerCubicMeter
{
    public interface IRatePerCubicMeterRepository
    {
        Task<IReadOnlyList<RatePerCubicMeter>> GetAllAsync();
        Task<RatePerCubicMeter?> GetByIdAsync(RatePerCubicMeterId id);
        Task<bool> ExistsAsync(RatePerCubicMeterId id);
        Task Add(RatePerCubicMeter ratePerCubicMeter);
        Task Update(RatePerCubicMeter ratePerCubicMeter);
        Task Delete(RatePerCubicMeter ratePerCubicMeter);
    }
}
