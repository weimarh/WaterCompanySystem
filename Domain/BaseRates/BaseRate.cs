using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.BaseRates
{
    public sealed class BaseRate : AggregateRoot
    {
        private BaseRate() { }

        public BaseRate(BaseRateId baseRateId, DateTime creationDate, Money amount)
        {
            BaseRateId = baseRateId;
            CreationDate = creationDate;
            Amount = amount;
        }

        public BaseRateId BaseRateId { get; private set; } = null!;
        public DateTime CreationDate { get; private set; }
        public Money Amount { get; private set; } = null!;

        public static BaseRate UpdateBaseRate(BaseRateId baseRateId, DateTime creationDate, Money amount)
        {
            return new BaseRate(baseRateId, creationDate, amount);
        }
    }
}
