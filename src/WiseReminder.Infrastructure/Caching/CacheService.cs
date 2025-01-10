namespace WiseReminder.Infrastructure.Caching;

public sealed class CacheService<TConverter>(
    IDistributedCache cache,
    TConverter converter)
    : ICacheService<TConverter>
    where TConverter : JsonConverter
{
    public async Task<TEntity> GetOrCreateAsync<TEntity>(
        string key,
        Func<Task<TEntity>> method,
        CancellationToken cancellationToken,
        TimeSpan? expiration = null)
    {
        var serialized = await cache.GetStringAsync(key, cancellationToken);

        if (serialized != null)
        {
            return Deserialize<TEntity>(serialized);
        }

        var entity = await method();

        if (entity != null)
        {
            await SetAsync(key, entity, cancellationToken, expiration);
        }

        return entity;
    }

    public async Task SetAsync<TEntity>(
        string key,
        TEntity entity,
        CancellationToken cancellationToken,
        TimeSpan? expiration = null)
    {
        var serialized = Serialize(entity);

        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = expiration ?? TimeSpan.FromMinutes(10),
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30)
        };

        await cache.SetStringAsync(key, serialized, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    private string Serialize<TEntity>(TEntity entity)
    {
        return JsonConvert.SerializeObject(entity, converter);
    }

    private TEntity Deserialize<TEntity>(string serialized)
    {
        return JsonConvert.DeserializeObject<TEntity>(serialized, converter)!;
    }
}