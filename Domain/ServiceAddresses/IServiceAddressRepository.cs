namespace Domain.ServiceAddresses
{
    public interface IServiceAddressRepository
    {
        Task<IReadOnlyList<ServiceAddress>> GetAllAsync();
        Task<ServiceAddress?> GetByIdAsync(ServiceAddressId id);
        Task<bool> ExistsAsync(ServiceAddressId id);
        Task Add(ServiceAddress serviceAddress);
        Task Update(ServiceAddress serviceAddress);
        Task Delete(ServiceAddress serviceAddress);
    }
}
