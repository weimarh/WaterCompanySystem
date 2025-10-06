namespace Domain.ServiceAddresses
{
    public interface IServiceAddressRepository
    {
        Task<IReadOnlyList<ServiceAddress>> GetAllAsync();
        Task<ServiceAddress?> GetByIdAsync(ServiceAddressId id);
        Task<bool> ExistsAsync(ServiceAddressId id);
        Task Add(ServiceAddress serviceAddress);
        void Update(ServiceAddress serviceAddress);
        void Delete(ServiceAddress serviceAddress);
    }
}
