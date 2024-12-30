namespace WiseReminder.Domain.Quotes;

public interface IQuoteRepository
{
    void CreateQuote(Quote quote);
    void UpdateQuote(Quote quote);
    void DeleteQuote(Quote quote);
    Task<Quote?> GetQuoteById(Guid id);
    Task<Quote> GetDailyQuote();
    Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId);
    Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId);
    Task<ICollection<Quote>> GetRandomQuotes(int amount);
    Task<ICollection<Quote>> GetRecentAddedQuotes(int amount);
}