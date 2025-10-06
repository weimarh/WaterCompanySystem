using Domain.Enums;
using Domain.ServiceAddresses;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.ServiceAddresses
{
    public class ServiceAddressConfiguration : IEntityTypeConfiguration<ServiceAddress>
    {
        public void Configure(EntityTypeBuilder<ServiceAddress> builder)
        {
            builder.ToTable("ServiceAddresses");

            builder.HasKey(sa => sa.ServiceAddressId);

            builder.Property(sa => sa.ServiceAddressId)
            .HasConversion(
                id => id.Value,
                value => new ServiceAddressId(value))
            .ValueGeneratedNever(); 

            builder.Property(sa => sa.StreetName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(sa => sa.HouseNumber).HasConversion(
                houseNumber => houseNumber.Value,
                value => HouseNumber.Create(value) ?? null!);

            // Configure RatePlan as enum or value object
            builder.Property(sa => sa.RatePlan)
                .HasConversion(
                    rp => rp.ToString(), // Convert enum to string
                    rp => (RatePlan)Enum.Parse(typeof(RatePlan), rp))
                .HasMaxLength(50)
                .IsRequired();

            // Configure one-to-many relationship with Invoices
            builder.HasMany(sa => sa.Invoices)
                .WithOne(i => i.ServiceAddress)
                .HasForeignKey(i => i.ServiceAddressId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for invoices
        }
    }
}
