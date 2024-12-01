namespace WiseReminder.Application.Abstractions.Caching;

public interface ICacheService
{
    Task CreateAsync<T>(string key, T entity) where T : class;
    Task<T?> GetAsync<T>(string key) where T : class;
}