using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.RatesPerCubicMeter
{
    public sealed class RatePerCubicMeter : AggregateRoot
    {
        private RatePerCubicMeter() { }

        public RatePerCubicMeter(RatePerCubicMeterId ratePerCubicMeterId, DateTime creationDate, Money amount)
        {
            RatePerCubicMeterId = ratePerCubicMeterId;
            CreationDate = creationDate;
            Amount = amount;
        }

        public RatePerCubicMeterId RatePerCubicMeterId { get; private set; } = null!;
        public DateTime CreationDate { get; set; }
        public Money Amount { get; set; } = null!;

        public static RatePerCubicMeter UpdateRatePerCubicMeter(RatePerCubicMeterId ratePerCubicMeterId, DateTime creationDate, Money amount)
        {
            return new RatePerCubicMeter
            (
                ratePerCubicMeterId,
                creationDate,
                amount
            );
        }
    }
}
