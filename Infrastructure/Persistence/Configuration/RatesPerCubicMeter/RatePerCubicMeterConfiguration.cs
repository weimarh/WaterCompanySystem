using Domain.RatesPerCubicMeter;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.RatesPerCubicMeter
{
    public class RatePerCubicMeterConfiguration : IEntityTypeConfiguration<RatePerCubicMeter>
    {
        public void Configure(EntityTypeBuilder<RatePerCubicMeter> builder)
        {
            builder.ToTable("Rates");

            builder.HasKey(r => r.RatePerCubicMeterId);

            builder.Property(r => r.RatePerCubicMeterId).HasConversion(
                conversionId => conversionId.Value,
                value => new RatePerCubicMeterId(value))
                .ValueGeneratedNever();

            builder.Property(r => r.CreationDate).IsRequired();

            builder.Property(r => r.Amount)
                .HasConversion(
                    amountConversion => amountConversion.Value,
                    value => Money.Create(value) ?? null!)
                .IsRequired();
        }
    }
}
