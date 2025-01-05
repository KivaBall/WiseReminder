namespace WiseReminder.Domain.Abstractions;

public abstract class Entity<T> where T : Entity<T>
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime AddedAt { get; } = DateTime.UtcNow;
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public T Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        return (T)this;
    }
}