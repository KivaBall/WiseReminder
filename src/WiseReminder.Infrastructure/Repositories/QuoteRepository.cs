namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(
    AppDbContext context,
    IQuoteService quoteService,
    ICacheService cacheService)
    : IQuoteRepository
{
    private readonly DbSet<Quote> _quotes = context.Quotes;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly ICacheService _cacheService = cacheService;

    public void CreateQuote(Quote quote)
    {
        _quotes.Add(quote);
    }

    public void UpdateQuote(Quote quote)
    {
        _quotes.Update(quote);
    }

    public void DeleteQuote(Quote quote)
    {
        _quoteService.DeleteQuote(quote);
        _quotes.Update(quote);
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        var key = $"quote-{id}";

        var quote = await _cacheService
            .GetAsync<Quote>(key);

        if (quote != null)
        {
            return quote;
        }

        var dbQuote = await _quotes
            .FirstOrDefaultAsync(q => q.Id == id);

        if (dbQuote != null)
        {
            await _cacheService
                .CreateAsync(key, dbQuote);
        }

        return dbQuote;
    }

    public async Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId)
    {
        var key = $"quote-category-{categoryId}";

        var quotes = await _cacheService
            .GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await _quotes
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync();

        await _cacheService
            .CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId)
    {
        var key = $"quote-author-{authorId}";

        var quotes = await _cacheService
            .GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await _quotes
            .Where(q => q.AuthorId == authorId)
            .ToListAsync();

        await _cacheService
            .CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<ICollection<Quote>> GetRandomQuotes(int amount)
    {
        return await _quotes
            .OrderBy(q => Guid.NewGuid())
            .Take(amount)
            .ToListAsync();
    }

    public async Task<ICollection<Quote>> GetRecentAddedQuotes(int amount)
    {
        return await _quotes
            .OrderByDescending(q => q.AddedAt)
            .Take(amount)
            .ToListAsync();
    }

    public async Task<Quote> GetQuoteOfTheDay()
    {
        var quote = await _cacheService
            .GetAsync<Quote>("quote-of-the-day");

        if (quote == null)
        {
            quote = await _quotes
                .OrderBy(q => Guid.NewGuid())
                .FirstAsync();

            await _cacheService
                .CreateAsync("quote-of-the-day", quote, TimeSpan.FromDays(1));
        }

        return quote;
    }
}