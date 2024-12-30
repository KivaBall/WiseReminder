namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(
    AppDbContext context,
    ICacheService cacheService)
    : IQuoteRepository
{
    public void CreateQuote(Quote quote)
    {
        context.Quotes.Add(quote);
    }

    public void UpdateQuote(Quote quote)
    {
        context.Quotes.Update(quote);
    }

    public void DeleteQuote(Quote quote)
    {
        quote.Delete();

        context.Quotes.Update(quote);
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        var key = $"quote-{id}";

        var quote = await cacheService.GetAsync<Quote>(key);

        if (quote != null)
        {
            return quote;
        }

        var dbQuote = await context.Quotes.FirstOrDefaultAsync(q => q.Id == id);

        if (dbQuote != null)
        {
            await cacheService.CreateAsync(key, dbQuote);
        }

        return dbQuote;
    }

    public async Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId)
    {
        var key = $"quote-category-{categoryId}";

        var quotes = await cacheService.GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await context.Quotes
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync();

        await cacheService.CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId)
    {
        var key = $"quote-author-{authorId}";

        var quotes = await cacheService.GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await context.Quotes
            .Where(q => q.AuthorId == authorId)
            .ToListAsync();

        await cacheService.CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<ICollection<Quote>> GetRandomQuotes(int amount)
    {
        return await context.Quotes
            .OrderBy(q => Guid.NewGuid())
            .Take(amount)
            .ToListAsync();
    }

    public async Task<ICollection<Quote>> GetRecentAddedQuotes(int amount)
    {
        return await context.Quotes
            .OrderByDescending(q => q.AddedAt)
            .Take(amount)
            .ToListAsync();
    }

    public async Task<Quote> GetDailyQuote()
    {
        var quote = await cacheService.GetAsync<Quote>("daily-quote");

        if (quote == null)
        {
            quote = await context.Quotes
                .OrderBy(q => Guid.NewGuid())
                .FirstAsync();

            await cacheService.CreateAsync("daily-quote", quote, TimeSpan.FromDays(1));
        }

        return quote;
    }
}