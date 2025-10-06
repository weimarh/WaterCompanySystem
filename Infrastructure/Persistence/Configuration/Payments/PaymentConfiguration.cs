using Domain.Enums;
using Domain.Payments;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Payments
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.PaymentId)
                .HasConversion(
                    id => id.Value,
                    value => new PaymentId(value))
                .ValueGeneratedNever(); // Since you're providing the ID

            builder.Property(p => p.BillingPeriod)
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            // Configure Money as a value object (owned entity)
            builder.Property(p => p.Amount).HasConversion(
                    amountConversion => amountConversion.Value,
                    value => Money.Create(value) ?? null!)
                .IsRequired();

            // Configure PaymentMethod as enum
            builder.Property(p => p.PaymentMethod)
                .HasConversion(
                    pm => pm.ToString(),
                    pm => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), pm))
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.InvoiceNumber)
                .IsRequired();

            // Invoice relationship (many-to-one) - Multiple payments can be for one invoice
            builder.HasOne(p => p.Invoice)
                .WithMany() // Assuming Invoice doesn't have navigation back to Payment
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Customer relationship (many-to-one)
            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // WaterMeter relationship (many-to-one)
            builder.HasOne(p => p.WaterMeter)
                .WithMany()
                .HasForeignKey(p => p.WaterMeterId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
