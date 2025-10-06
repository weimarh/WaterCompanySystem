using Domain.Customers;
using Domain.ServiceAddresses;
using Domain.WaterMeters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.WaterMeters
{
    public class WaterMeterConfiguration : IEntityTypeConfiguration<WaterMeter>
    {
        public void Configure(EntityTypeBuilder<WaterMeter> builder)
        {
            builder.ToTable("WaterMeters");

            builder.HasKey(w => w.WaterMeterId);

            builder.Property(w => w.WaterMeterId).HasConversion(
                id => id.Value,
                value => new WaterMeterId(value))
                .ValueGeneratedNever();

            builder.Property(w => w.Model)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(w => w.InstallationDate)
                .IsRequired();

            builder.HasOne<Customer>()
                .WithMany(c => c.WaterMeters)
                .HasForeignKey(c => c.WaterMeterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-one relationship with ServiceAddress
            builder.HasOne<ServiceAddress>()
                .WithOne()
                .HasForeignKey<WaterMeter>(w => w.ServiceAddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Readings)
                .WithOne(r => r.WaterMeter)
                .HasForeignKey(r => r.WaterMeterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Invoices)
                .WithOne(i => i.WaterMeter)
                .HasForeignKey(i => i.WaterMeterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
