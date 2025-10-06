namespace Domain.RatesPerCubicMeter
{
    public interface IRatePerCubicMeterRepository
    {
        Task<IReadOnlyList<RatePerCubicMeter>> GetAllAsync();
        Task<RatePerCubicMeter?> GetByIdAsync(RatePerCubicMeterId id);
        Task<bool> ExistsAsync(RatePerCubicMeterId id);
        Task Add(RatePerCubicMeter ratePerCubicMeter);
        void Update(RatePerCubicMeter ratePerCubicMeter);
        void Delete(RatePerCubicMeter ratePerCubicMeter);
    }
}
