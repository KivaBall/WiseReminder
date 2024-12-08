namespace WiseReminder.Domain.Quotes;

public sealed class QuoteService : IQuoteService
{
    public Quote CreateQuote(QuoteText text, Guid authorId, Author? author, Guid categoryId, Category? category,
        QuoteDate quoteDate)
    {
        return new Quote(text, authorId, author!, categoryId, category!, quoteDate);
    }

    public Quote UpdateQuote(Quote quote, QuoteText text, Guid authorId, Author author, Guid categoryId,
        Category category, QuoteDate quoteDate)
    {
        quote.Text = text;
        quote.AuthorId = authorId;
        quote.Author = author;
        quote.CategoryId = categoryId;
        quote.Category = category;
        quote.QuoteDate = quoteDate;
        return quote;
    }

    public Quote DeleteQuote(Quote quote)
    {
        return (Quote)quote.DeleteEntity();
    }
}