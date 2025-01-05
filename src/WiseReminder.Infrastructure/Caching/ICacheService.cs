namespace WiseReminder.Infrastructure.Caching;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T?>> entityFunc, TimeSpan? expiration = null);
    Task SetAsync<T>(string key, T entity);
    Task RemoveAsync(string key);
}