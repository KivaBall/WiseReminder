namespace WiseReminder.Infrastructure.Caching;

public sealed class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T?>> entityFunc,
        TimeSpan? expiration = null)
    {
        var serializedObject = await cache.GetStringAsync(key);

        if (serializedObject != null)
        {
            var deserializedObject =
                JsonConvert.DeserializeObject<T>(serializedObject, new QuoteConverter());

            if (deserializedObject == null)
            {
                throw new Exception($"Could not deserialize object of type {typeof(T).Name}");
            }

            return deserializedObject;
        }

        var entity = await entityFunc();

        if (entity == null)
        {
            return entity!;
        }

        var createdSerializedObject = JsonConvert.SerializeObject(entity, new QuoteConverter());

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
        };

        await cache.SetStringAsync(key, createdSerializedObject, options);

        return entity;
    }

    public async Task SetAsync<T>(string key, T entity)
    {
        var createdSerializedObject = JsonConvert.SerializeObject(entity, new QuoteConverter());

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        await cache.SetStringAsync(key, createdSerializedObject, options);
    }

    public async Task RemoveAsync(string key)
    {
        await cache.RemoveAsync(key);
    }
}