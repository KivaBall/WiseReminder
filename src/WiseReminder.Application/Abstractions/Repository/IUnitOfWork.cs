namespace WiseReminder.Application.Abstractions.Repository;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken);

    Task<Result<TResult>> SaveChangesAsync<TResult>(Func<TResult> entity,
        CancellationToken cancellationToken);
}