namespace WiseReminder.Domain.Quotes;

public interface IQuoteRepository
{
    void CreateQuote(Quote quote);

    Task UpdateQuote(Quote quote);

    Task DeleteQuote(Quote quote);

    Task<Quote?> GetQuoteById(Guid id, CancellationToken cancellationToken);

    Task<QuoteDetails?> GetQuoteDetailsById(Guid id, CancellationToken cancellationToken);

    Task<bool> HasQuoteById(Guid id, CancellationToken cancellationToken);

    Task<QuoteDetails> GetDailyQuote(CancellationToken cancellationToken);

    Task<QuoteDetails> GetWeeklyQuote(CancellationToken cancellationToken);

    Task<QuoteDetails> GetMonthlyQuote(CancellationToken cancellationToken);

    Task<ICollection<QuoteDetails>>
        GetRandomQuotes(int amount, CancellationToken cancellationToken);

    Task<ICollection<QuoteDetails>> GetRecentAddedQuotes(int amount,
        CancellationToken cancellationToken);

    Task<int> GetQuotesAmountByAuthorId(Guid authorId, CancellationToken cancellationToken);

    Task<ICollection<Quote>> GetQuotesByClauses(Guid? categoryId, Guid? authorId,
        ICollection<string>? keywords, CancellationToken cancellationToken);

    Task<ICollection<QuoteDetails>> GetQuoteDetailsByClauses(Guid? categoryId, Guid? authorId,
        ICollection<string>? keywords, CancellationToken cancellationToken);
}