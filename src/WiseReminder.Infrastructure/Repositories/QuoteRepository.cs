namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(AppDbContext context, IQuoteService quoteService, ICacheService cacheService)
    : IQuoteRepository
{
    private readonly AppDbContext _context = context;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly ICacheService _cacheService = cacheService;

    public void CreateQuote(Quote quote)
    {
        _context.Quotes.Add(quote);
    }

    public void UpdateQuote(Quote quote)
    {
        _context.Quotes.Update(quote);
    }

    public void DeleteQuote(Quote quote)
    {
        _quoteService.DeleteQuote(quote);
        _context.Quotes.Update(quote);
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        var key = $"quote-{id}";

        var quote = await _cacheService.GetAsync<Quote>(key);

        if (quote != null)
        {
            return quote;
        }

        var dbQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id);

        if (dbQuote != null)
        {
            await _cacheService.CreateAsync(key, dbQuote);
        }

        return dbQuote;
    }

    public async Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId)
    {
        var key = $"quote-category-{categoryId}";

        var quotes = await _cacheService.GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await _context.Quotes.Where(quote => quote.CategoryId == categoryId).ToListAsync();

        await _cacheService.CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId)
    {
        var key = $"quote-author-{authorId}";

        var quotes = await _cacheService.GetAsync<ICollection<Quote>>(key);

        if (quotes != null)
        {
            return quotes;
        }

        var dbQuotes = await _context.Quotes.Where(quote => quote.AuthorId == authorId).ToListAsync();

        await _cacheService.CreateAsync(key, dbQuotes);

        return dbQuotes;
    }

    public async Task<Quote> GetRandomQuote()
    {
        return await _context.Quotes.OrderBy(quote => Guid.NewGuid()).FirstAsync();
    }
}