namespace Domain.BaseRates
{
    public interface IBaseRateRepository
    {
        Task<IReadOnlyList<BaseRate>> GetAllAsync();
        Task<BaseRate?> GetByIdAsync(BaseRateId id);
        Task<bool> ExistsAsync(BaseRateId id);
        Task Add(BaseRate baseRate);
        void Update(BaseRate baseRate);
        void Delete(BaseRate baseRate);
    }
}
