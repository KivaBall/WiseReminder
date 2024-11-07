using Microsoft.EntityFrameworkCore;
using WiseReminder.Domain.Quotes;
using WiseReminder.Infrastructure.Data;

namespace WiseReminder.Infrastructure.Repositories;

public sealed class QuoteRepository(AppDbContext context, IQuoteService quoteService) : IQuoteRepository
{
    private readonly AppDbContext _context = context;
    private readonly IQuoteService _quoteService = quoteService;

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
        return await _context.Quotes.FirstOrDefaultAsync(quote => quote.Id == id);
    }

    public async Task<ICollection<Quote>?> GetQuotesByCategoryId(Guid categoryId)
    {
        return await _context.Quotes.Where(quote => quote.CategoryId == categoryId).ToListAsync();
    }

    public async Task<ICollection<Quote>?> GetQuotesByAuthorId(Guid authorId)
    {
        return await _context.Quotes.Where(quote => quote.AuthorId == authorId).ToListAsync();
    }
}