namespace WiseReminder.Domain.Abstractions;

public abstract class Entity
{
    internal Entity()
    {
        Id = Guid.NewGuid();
        AddedAt = DateTime.Now;
    }

    public Guid Id { get; }
    public DateTime AddedAt { get; }
    public DateTime? DeletedAt { get; private set; }

    internal Entity DeleteEntity()
    {
        DeletedAt = DateTime.Now;
        return this;
    }
}