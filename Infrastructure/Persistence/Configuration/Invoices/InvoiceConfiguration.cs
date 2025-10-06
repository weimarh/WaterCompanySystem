using Domain.Invoices;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Invoices
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(i => i.InvoiceId);

            builder.Property(i => i.InvoiceId)
                .HasConversion(
                    id => id.Value,
                    value => new InvoiceId(value))
                .ValueGeneratedNever(); // Since you're providing the ID

            builder.Property(i => i.InvoiceNumber)
                .IsRequired();

            // Ensure unique invoice numbers
            builder.HasIndex(i => i.InvoiceNumber)
                .IsUnique();

            builder.Property(i => i.BillingPeriod)
                .IsRequired();

            // Configure Money as a value object (owned entity)
            builder.Property(i => i.TotalAmountDue).HasConversion(
                amount => amount.Value,
                value => Money.Create(value) ?? null!);

            builder.Property(i => i.DueDate)
                .IsRequired();

            builder.Property(i => i.IsPaid)
                .IsRequired()
                .HasDefaultValue(false); // Default to unpaid

            // Customer relationship (many-to-one)
            builder.HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // WaterMeter relationship (many-to-one)
            builder.HasOne(i => i.WaterMeter)
                .WithMany(w => w.Invoices)
                .HasForeignKey(i => i.WaterMeterId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Reading relationship (one-to-one) - Each invoice is for a specific reading
            builder.HasOne(i => i.Reading)
                .WithOne() // Assuming Reading doesn't have navigation back to Invoice
                .HasForeignKey<Invoice>(i => i.ReadingId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // ServiceAddress relationship (many-to-one)
            builder.HasOne(i => i.ServiceAddress)
                .WithMany(sa => sa.Invoices)
                .HasForeignKey(i => i.ServiceAddressId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
