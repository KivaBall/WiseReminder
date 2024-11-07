namespace WiseReminder.Domain.Quotes;

public interface IQuoteRepository
{
    void CreateQuote(Quote quote);
    void UpdateQuote(Quote quote);
    void DeleteQuote(Quote quote);
    Task<Quote?> GetQuoteById(Guid id);
    Task<ICollection<Quote>> GetQuotesByCategoryId(Guid categoryId);
    Task<ICollection<Quote>> GetQuotesByAuthorId(Guid authorId);
}