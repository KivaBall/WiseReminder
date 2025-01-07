namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(
    AppDbContext context)
    : IQuoteRepository
{
    public void CreateQuote(Quote quote)
    {
        context.Quotes.Add(quote);
    }

    public Task UpdateQuote(Quote quote)
    {
        context.Quotes.Update(quote);

        return Task.CompletedTask;
    }

    public Task DeleteQuote(Quote quote)
    {
        quote.Delete();

        context.Quotes.Update(quote);

        return Task.CompletedTask;
    }

    public async Task<Quote?> GetQuoteById(Guid id)
    {
        return await context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId)
    {
        return await context.Quotes
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId)
    {
        return await context.Quotes
            .Where(q => q.AuthorId == authorId)
            .ToListAsync();
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

    public async Task<int> GetNumberOfQuotesByAuthorId(Guid authorId)
    {
        return await context.Quotes.CountAsync(q => q.AuthorId == authorId);
    }

    public async Task<Quote> GetDailyQuote()
    {
        return await context.Quotes
            .OrderBy(q => Guid.NewGuid())
            .FirstAsync();
    }
}