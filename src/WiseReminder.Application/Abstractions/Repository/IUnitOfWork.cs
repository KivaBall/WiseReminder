namespace WiseReminder.Application.Abstractions.Repository;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync();
}