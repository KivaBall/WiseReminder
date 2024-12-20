namespace WiseReminder.IntegrationTests.Abstractions.Mocks;

public sealed class FakeMemoryCache : IMemoryCache
{
    public ICacheEntry CreateEntry(object key)
    {
        return new FakeCacheEntry();
    }

    public void Dispose()
    {
    }

    public void Remove(object key)
    {
    }

    public bool TryGetValue(object key, out object? value)
    {
        value = null;
        return false;
    }

    private class FakeCacheEntry : ICacheEntry
    {
        public object Key { get; } = null!;
        public object? Value { get; set; } = null;
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public long? Size { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
        public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; } =
            new List<PostEvictionCallbackRegistration>();

        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

        public void Dispose()
        {
        }
    }
}