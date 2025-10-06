using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Customers
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.CustomerId);

            builder.Property(x => x.CustomerId).HasConversion(
                conversionId => conversionId.Value,
                value => new CustomerId(value))
                .ValueGeneratedNever();

            builder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.PhoneNumber).HasConversion(
                converion => converion.Value,
                value => PhoneNumber.Create(value) ?? null!)
                .HasMaxLength(8)
                .IsRequired();

            // Configure one-to-many relationship with WaterMeter
            builder.HasMany(c => c.WaterMeters)
                .WithOne(w => w.Customer) // If WaterMeter has a Customer navigation property, use: .WithOne(w => w.Customer)
                .HasForeignKey("CustomerId") // Use shadow property if no explicit FK in WaterMeter
                .OnDelete(DeleteBehavior.Cascade); // Or Restrict, SetNull based on business rules

            // Configure one-to-many relationship with Invoice
            builder.HasMany(c => c.Invoices)
                .WithOne(i => i.Customer) // If Invoice has a Customer navigation property, use: .WithOne(i => i.Customer)
                .HasForeignKey("CustomerId") // Use shadow property if no explicit FK in Invoice
                .OnDelete(DeleteBehavior.Cascade); // Or Restrict, SetNull based on business rules
        }
    }
}
