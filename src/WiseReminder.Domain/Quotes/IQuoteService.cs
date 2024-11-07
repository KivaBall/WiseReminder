using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Domain.Quotes;

public interface IQuoteService
{
    Quote CreateQuote(QuoteText text, Guid authorId, Author author, Guid categoryId, Category category,
        QuoteDate quoteDate);

    Quote UpdateQuote(Quote quote, QuoteText text, Guid authorId, Author author, Guid categoryId, Category category,
        QuoteDate quoteDate);

    Quote DeleteQuote(Quote quote);
}