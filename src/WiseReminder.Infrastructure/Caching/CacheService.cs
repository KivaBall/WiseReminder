﻿namespace WiseReminder.Infrastructure.Caching;

public sealed class CacheService(IDistributedCache distributedCache) : ICacheService
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task CreateAsync<T>(string key, T entity) where T : class
    {
        var obj = JsonConvert.SerializeObject(entity);

        await _distributedCache.SetStringAsync(key, obj);
    }

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var obj = await _distributedCache.GetStringAsync(key);

        if (obj == null)
        {
            return null;
        }

        var deserializedObj = JsonConvert.DeserializeObject<T>(obj, new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver()
        });

        return deserializedObj;
    }
}