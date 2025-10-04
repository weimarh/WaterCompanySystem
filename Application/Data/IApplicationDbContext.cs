using Domain.BaseRates;
using Domain.Customers;
using Domain.Invoices;
using Domain.Payments;
using Domain.RatesPerCubicMeter;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Reading> Readings { get; set; }
        DbSet<ServiceAddress> ServiceAddresses { get; set; }
        DbSet<WaterMeter> WaterMeters { get; set; }
        DbSet<BaseRate> BaseRates { get; set; }
        DbSet<RatePerCubicMeter> RatePerCubicMeters { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
