using Domain.Primitives;
using Domain.ValueObjects;
using Domain.WaterMeters;

namespace Domain.Readings
{
    public sealed class Reading : AggregateRoot
    {
        private Reading() { }

        public Reading(ReadingId readingId, DateTime readingDate, ReadingValue readingValue, WaterMeterId waterMeterId, WaterMeter waterMeter)
        {
            ReadingId = readingId;
            ReadingDate = readingDate;
            ReadingValue = readingValue;
            WaterMeterId = waterMeterId;
            WaterMeter = waterMeter;
        }

        public ReadingId ReadingId { get; private set; } = null!;
        public DateTime ReadingDate { get; private set; }
        public ReadingValue ReadingValue { get; private set; } = null!;
        public WaterMeterId WaterMeterId { get; private set; } = null!;
        public WaterMeter WaterMeter { get; private set; } = null!;

        public static Reading UpdateReading(ReadingId readingId, DateTime readingDate, ReadingValue readingValue, WaterMeterId waterMeterId, WaterMeter waterMeter)
        {
            return new Reading(readingId, readingDate, readingValue, waterMeterId, waterMeter);
        }
    }
}
