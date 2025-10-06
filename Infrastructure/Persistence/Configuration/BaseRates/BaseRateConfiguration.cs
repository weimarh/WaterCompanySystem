using Domain.BaseRates;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.BaseRates
{
    public class BaseRateConfiguration : IEntityTypeConfiguration<BaseRate>
    {
        public void Configure(EntityTypeBuilder<BaseRate> builder)
        {
            builder.ToTable("BaseRates");

            builder.HasKey(b => b.BaseRateId);

            builder.Property(b => b.BaseRateId).HasConversion(
                conversionId => conversionId.Value,
                value => new BaseRateId(value))
                .ValueGeneratedNever();

            builder.Property(b => b.CreationDate).IsRequired();

            builder.Property(b => b.Amount)
                .HasConversion(
                    amountConversion => amountConversion.Value,
                    value => Money.Create(value) ?? null!) 
                .IsRequired();
        }
    }
}
