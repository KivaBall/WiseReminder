using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Domain.Quotes;

public sealed class Quote : Entity
{
    internal Quote(QuoteText text, Guid authorId, Author author, Guid categoryId, Category category, QuoteDate quoteDate)
    {
        Text = text;
        AuthorId = authorId;
        Author = author;
        CategoryId = categoryId;
        Category = category;
        QuoteDate = quoteDate;
    }

    // ReSharper disable once UnusedMember.Local
    private Quote()
    {
    }

    public QuoteText Text { get; internal set; }
    public Guid AuthorId { get; internal set; }
    public Author Author { get; internal set; }
    public Guid CategoryId { get; internal set; }
    public Category Category { get; internal set; }
    public QuoteDate QuoteDate { get; internal set; }
}