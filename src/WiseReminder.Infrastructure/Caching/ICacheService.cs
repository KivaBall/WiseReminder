namespace WiseReminder.Infrastructure.Caching;

// ReSharper disable once UnusedTypeParameter
public interface ICacheService<TConverter> where TConverter : JsonConverter
{
    Task<TEntity> GetOrCreateAsync<TEntity>(string key, Func<Task<TEntity>> method,
        CancellationToken cancellationToken, TimeSpan? expiration = null);

    Task SetAsync<TEntity>(string key, TEntity entity, CancellationToken cancellationToken,
        TimeSpan? expiration = null);

    Task RemoveAsync(string key, CancellationToken cancellationToken);
}