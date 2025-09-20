namespace Domain.ServiceAddresses
{
    public interface IServiceAddress
    {
        Task<IReadOnlyList<ServiceAddress>> GetAllAsync();
        Task<ServiceAddress?> GetByIdAsync(ServiceAddressId id);
        Task<bool> ExistsAsync(ServiceAddressId id);
        Task Add(ServiceAddress customer);
        Task Update(ServiceAddress customer);
        Task Delete(ServiceAddress customer);
    }
}
