using Application.Data;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IApplicationDbContext _context;

        public CustomerRepository(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);

        public void Delete(Customer customer) => _context.Customers.Remove(customer);

        public async Task<bool> ExistsAsync(CustomerId id) => await _context.Customers.AnyAsync(c => c.CustomerId == id);

        public async Task<IReadOnlyList<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();
        public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == id);

        public void Update(Customer customer) => _context.Customers.Update(customer);
    }
}
