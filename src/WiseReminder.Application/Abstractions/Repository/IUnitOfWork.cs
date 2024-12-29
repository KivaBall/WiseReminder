namespace WiseReminder.Application.Abstractions.Repository;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync();
    Task<Result<TResult>> SaveChangesAsyncWithResult<TResult>(Func<TResult> entity);
}