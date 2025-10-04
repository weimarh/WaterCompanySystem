namespace Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OcurredOn { get; }
    }
}
