namespace WiseReminder.Infrastructure.Repositories;

public sealed class CachedQuoteRepository(
    [FromKeyedServices("original-quote-repository")]
    IQuoteRepository repository,
    ICacheService<QuoteConverter> cache)
    : IQuoteRepository
{
    public void CreateQuote(Quote quote)
    {
        repository.CreateQuote(quote);
    }

    public async Task UpdateQuote(Quote quote, CancellationToken cancellationToken)
    {
        await repository.UpdateQuote(quote, cancellationToken);

        await cache.SetAsync(quote.Id.ToString(), quote, cancellationToken);
    }

    public async Task DeleteQuote(Quote quote, CancellationToken cancellationToken)
    {
        await repository.DeleteQuote(quote, cancellationToken);

        await cache.RemoveAsync(quote.Id.ToString(), cancellationToken);
    }

    public async Task<Quote?> GetQuoteById(Guid id, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(id.ToString(),
            async () => await repository.GetQuoteById(id, cancellationToken), cancellationToken);
    }

    public async Task<bool> HasQuoteById(Guid id, CancellationToken cancellationToken)
    {
        return await repository.HasQuoteById(id, cancellationToken);
    }

    public async Task<QuoteDetails> GetDailyQuote(CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync("daily",
            async () => await repository.GetDailyQuote(cancellationToken), cancellationToken);
    }

    public async Task<QuoteDetails> GetWeeklyQuote(CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync("weekly",
            async () => await repository.GetWeeklyQuote(cancellationToken), cancellationToken);
    }

    public async Task<QuoteDetails> GetMonthlyQuote(CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync("monthly",
            async () => await repository.GetMonthlyQuote(cancellationToken), cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetRandomQuotes(int amount,
        CancellationToken cancellationToken)
    {
        return await repository.GetRandomQuotes(amount, cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetRecentAddedQuotes(int amount,
        CancellationToken cancellationToken)
    {
        return await repository.GetRecentAddedQuotes(amount, cancellationToken);
    }

    public async Task<int> GetQuotesAmountByAuthorId(Guid authorId,
        CancellationToken cancellationToken)
    {
        return await repository.GetQuotesAmountByAuthorId(authorId, cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetQuoteDetailsByClauses(Guid? categoryId,
        Guid? authorId, ICollection<string>? keywords, CancellationToken cancellationToken)
    {
        var categoryString = categoryId == null ? "null" : categoryId.ToString()!;

        var authorString = authorId == null ? "null" : authorId.ToString()!;

        var keywordsString = keywords == null ? "null" : string.Join(",", keywords);

        var key = $"{categoryString}-{authorString}-{keywordsString}-dto";

        return await cache.GetOrCreateAsync(
            key,
            async () =>
                await repository.GetQuoteDetailsByClauses(categoryId, authorId, keywords,
                    cancellationToken),
            cancellationToken);
    }

    public async Task<ICollection<Quote>> GetQuotesByClauses(Guid? categoryId,
        Guid? authorId, ICollection<string>? keywords, CancellationToken cancellationToken)
    {
        var categoryString = categoryId == null ? "null" : categoryId.ToString()!;

        var authorString = authorId == null ? "null" : authorId.ToString()!;

        var keywordsString = keywords == null ? "null" : string.Join(",", keywords);

        var key = $"{categoryString}-{authorString}-{keywordsString}";

        return await cache.GetOrCreateAsync(
            key,
            async () =>
                await repository.GetQuotesByClauses(categoryId, authorId, keywords,
                    cancellationToken),
            cancellationToken);
    }

    public async Task<QuoteDetails?> GetQuoteDetailsById(Guid id,
        CancellationToken cancellationToken)
    {
        return await repository.GetQuoteDetailsById(id, cancellationToken);
    }
}