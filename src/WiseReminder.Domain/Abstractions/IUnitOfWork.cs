namespace WiseReminder.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}