namespace WiseReminder.Application.Abstractions.Repository;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken);

    Task<Result<T>> SaveChangesAsync<T>(T entity, CancellationToken cancellationToken);
}