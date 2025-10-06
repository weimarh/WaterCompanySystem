namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<IReadOnlyList<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(CustomerId id);
        Task<bool> ExistsAsync(CustomerId id);
        Task Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
