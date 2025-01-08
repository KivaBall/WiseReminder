namespace WiseReminder.Domain.Quotes;

public interface IQuoteRepository
{
    void CreateQuote(Quote quote);
    Task UpdateQuote(Quote quote);
    Task DeleteQuote(Quote quote);
    Task<Quote?> GetQuoteById(Guid id, CancellationToken cancellationToken);
    Task<bool> HasQuoteById(Guid id, CancellationToken cancellationToken);
    Task<Quote> GetDailyQuote(CancellationToken cancellationToken);
    Task<Quote> GetWeeklyQuote(CancellationToken cancellationToken);
    Task<Quote> GetMonthlyQuote(CancellationToken cancellationToken);

    Task<ICollection<Quote>> GetQuotesByClauses(Guid? categoryId, Guid? authorId,
        ICollection<string>? keywords, CancellationToken cancellationToken);

    Task<ICollection<Quote>> GetRandomQuotes(int amount, CancellationToken cancellationToken);
    Task<ICollection<Quote>> GetRecentAddedQuotes(int amount, CancellationToken cancellationToken);
    Task<int> GetQuotesAmountByAuthorId(Guid authorId, CancellationToken cancellationToken);
}