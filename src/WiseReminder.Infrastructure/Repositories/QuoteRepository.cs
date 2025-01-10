namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(
    AppDbContext context)
    : IQuoteRepository
{
    public void CreateQuote(Quote quote)
    {
        context.Quotes.Add(quote);
    }

    public Task UpdateQuote(Quote quote, CancellationToken cancellationToken)
    {
        context.Quotes.Update(quote);

        return Task.CompletedTask;
    }

    public Task DeleteQuote(Quote quote, CancellationToken cancellationToken)
    {
        quote.Delete();

        context.Quotes.Update(quote);

        return Task.CompletedTask;
    }

    public async Task<Quote?> GetQuoteById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<bool> HasQuoteById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Quotes
            .AnyAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetRandomQuotes(int amount,
        CancellationToken cancellationToken)
    {
        return await context.Quotes
            .OrderBy(q => Guid.NewGuid())
            .Take(amount)
            .ConvertToQuoteDetails(context)
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetRecentAddedQuotes(int amount,
        CancellationToken cancellationToken)
    {
        return await context.Quotes
            .OrderByDescending(q => q.AddedAt)
            .Take(amount)
            .ConvertToQuoteDetails(context)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetQuotesAmountByAuthorId(Guid authorId,
        CancellationToken cancellationToken)
    {
        return await context.Quotes
            .CountAsync(q => q.AuthorId == authorId, cancellationToken);
    }

    public async Task<QuoteDetails?> GetQuoteDetailsById(Guid id,
        CancellationToken cancellationToken)
    {
        return await context.Quotes
            .Where(q => q.Id == id)
            .ConvertToQuoteDetails(context)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<QuoteDetails>> GetQuoteDetailsByClauses(Guid? categoryId,
        Guid? authorId, ICollection<string>? keywords, CancellationToken cancellationToken)
    {
        var quotes = context.Quotes.AsQueryable();

        if (categoryId != null)
        {
            quotes = quotes.Where(q => q.CategoryId == categoryId);
        }

        if (authorId != null)
        {
            quotes = quotes.Where(q => q.AuthorId == authorId);
        }

        var quoteDetails = quotes.ConvertToQuoteDetails(context);

        if (keywords != null && keywords.Any())
        {
            var list = await quoteDetails
                .ToListAsync(cancellationToken);

            return list
                .Where(quote => keywords.Any(str =>
                    quote.Quote.Text.Value.Contains(str, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        return await quoteDetails
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Quote>> GetQuotesByClauses(Guid? categoryId, Guid? authorId,
        ICollection<string>? keywords, CancellationToken cancellationToken)
    {
        var quotes = context.Quotes.AsQueryable();

        if (categoryId != null)
        {
            quotes = quotes.Where(q => q.CategoryId == categoryId);
        }

        if (authorId != null)
        {
            quotes = quotes.Where(q => q.AuthorId == authorId);
        }

        if (keywords != null && keywords.Any())
        {
            var list = await quotes
                .ToListAsync(cancellationToken);

            return list
                .Where(quote => keywords.Any(str =>
                    quote.Text.Value.Contains(str, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        return await quotes
            .ToListAsync(cancellationToken);
    }

    public async Task<QuoteDetails> GetDailyQuote(CancellationToken cancellationToken)
    {
        return await context.Quotes
            .GetTopQuote(context, TimeSpan.FromDays(1))
            .ConvertToQuoteDetails(context)
            .FirstAsync(cancellationToken);
    }

    public async Task<QuoteDetails> GetWeeklyQuote(CancellationToken cancellationToken)
    {
        return await context.Quotes
            .GetTopQuote(context, TimeSpan.FromDays(7))
            .ConvertToQuoteDetails(context)
            .FirstAsync(cancellationToken);
    }

    public async Task<QuoteDetails> GetMonthlyQuote(CancellationToken cancellationToken)
    {
        return await context.Quotes
            .GetTopQuote(context, TimeSpan.FromDays(31))
            .ConvertToQuoteDetails(context)
            .FirstAsync(cancellationToken);
    }
}