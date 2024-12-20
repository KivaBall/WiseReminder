namespace WiseReminder.IntegrationTests.Abstractions.Mocks;

public sealed class FakeCacheService : ICacheService
{
    public Task CreateAsync<T>(string key, T entity, TimeSpan? time = null) where T : notnull
    {
        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key) where T : class
    {
        return Task.FromResult((T?)null);
    }
}