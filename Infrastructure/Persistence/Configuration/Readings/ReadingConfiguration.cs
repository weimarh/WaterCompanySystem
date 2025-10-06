using Domain.Readings;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Readings
{
    public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
    {
        public void Configure(EntityTypeBuilder<Reading> builder)
        {
            builder.ToTable("Readings");

            builder.HasKey(r => r.ReadingId);

            builder.Property(r => r.ReadingId)
                .HasConversion(
                    id => id.Value,
                    value => new ReadingId(value))
                .ValueGeneratedNever(); // Since you're providing the ID

            builder.Property(r => r.ReadingDate)
                .IsRequired();

            // Configure ReadingValue as a value object (owned entity)
            builder.Property(r => r.ReadingValue).HasConversion(
                readingValue => readingValue.Value,
                value => ReadingValue.Create(value) ?? null!);

            // WaterMeter relationship (many-to-one)
            builder.HasOne(r => r.WaterMeter)
                .WithMany(w => w.Readings)
                .HasForeignKey(r => r.WaterMeterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
