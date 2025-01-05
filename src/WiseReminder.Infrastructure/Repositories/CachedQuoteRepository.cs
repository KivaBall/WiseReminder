namespace WiseReminder.Infrastructure.Repositories;

public sealed class CachedQuoteRepository(
    [FromKeyedServices("original-quote-repository")]
    IQuoteRepository repository,
    ICacheService cache)
    : IQuoteRepository
{
    public void CreateQuote(Quote quote)
    {
        repository.CreateQuote(quote);
    }

    public async Task UpdateQuote(Quote quote)
    {
        await repository.UpdateQuote(quote);

        await cache.SetAsync(quote.Id.ToString(), quote);
    }

    public async Task DeleteQuote(Quote quote)
    {
        await repository.DeleteQuote(quote);

        await cache.RemoveAsync(quote.Id.ToString());
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        return await cache.GetOrCreateAsync(id.ToString(),
            async () => await repository.GetQuoteById(id));
    }

    public async Task<Quote> GetDailyQuote()
    {
        return await cache.GetOrCreateAsync("daily",
            async () => await repository.GetDailyQuote(),
            TimeSpan.FromDays(1));
    }

    public async Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId)
    {
        return await cache.GetOrCreateAsync($"by-category-{categoryId}",
            async () => await repository.GetQuotesByCategoryId(categoryId));
    }

    public async Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId)
    {
        return await cache.GetOrCreateAsync($"by-author-{authorId}",
            async () => await repository.GetQuotesByAuthorId(authorId));
    }

    public async Task<ICollection<Quote>> GetRandomQuotes(int amount)
    {
        return await repository.GetRandomQuotes(amount);
    }

    public async Task<ICollection<Quote>> GetRecentAddedQuotes(int amount)
    {
        return await repository.GetRecentAddedQuotes(amount);
    }
}