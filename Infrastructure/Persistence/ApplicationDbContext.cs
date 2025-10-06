using Application.Data;
using Domain.BaseRates;
using Domain.Customers;
using Domain.Invoices;
using Domain.Payments;
using Domain.Primitives;
using Domain.RatesPerCubicMeter;
using Domain.Readings;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(IPublisher publisher)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public DbSet<Customer> Customers { get ; set ; }
        public DbSet<Invoice> Invoices { get ; set ; }
        public DbSet<Payment> Payments { get ; set ; }
        public DbSet<Reading> Readings { get ; set ; }
        public DbSet<ServiceAddress> ServiceAddresses { get ; set ; }
        public DbSet<WaterMeter> WaterMeters { get ; set ; }
        public DbSet<BaseRate> BaseRates { get ; set ; }
        public DbSet<RatePerCubicMeter> RatePerCubicMeters { get ; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
