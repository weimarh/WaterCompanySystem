namespace Domain.Readings
{
    public interface IReadingRepository
    {
        Task<IReadOnlyList<Reading>> GetAllAsync();
        Task<Reading?> GetByIdAsync(ReadingId id);
        Task<bool> ExistsAsync(ReadingId id);
        Task Add(Reading customer);
        Task Update(Reading customer);
        Task Delete(Reading customer);
    }
}
