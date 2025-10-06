using Domain.ServiceAddresses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.ServiceAddresses
{
    public class ServiceAddressRepository : IServiceAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceAddressRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(ServiceAddress serviceAddress) => await _context.ServiceAddresses.AddAsync(serviceAddress);

        public void Delete(ServiceAddress serviceAddress) => _context.ServiceAddresses.Remove(serviceAddress);

        public async Task<bool> ExistsAsync(ServiceAddressId id) => await _context.ServiceAddresses.AnyAsync(s => s.ServiceAddressId == id);

        public async Task<IReadOnlyList<ServiceAddress>> GetAllAsync() => await _context.ServiceAddresses.ToListAsync();

        public async Task<ServiceAddress?> GetByIdAsync(ServiceAddressId id) => await _context.ServiceAddresses.FirstOrDefaultAsync(s => s.ServiceAddressId == id);

        public void Update(ServiceAddress serviceAddress) => _context?.ServiceAddresses.Update(serviceAddress);
    }
}
