namespace WiseReminder.Domain.Quotes;

public sealed class Quote : Entity<Quote>
{
    private Quote(Text text, Author author, Category category, Date quoteDate)
    {
        Text = text;
        AuthorId = author.Id;
        Author = author;
        CategoryId = category.Id;
        Category = category;
        QuoteDate = quoteDate;
    }

    // ReSharper disable once UnusedMember.Local
    private Quote()
    {
    }

    public Text Text { get; private set; }
    public Date QuoteDate { get; private set; }

    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }

    public static Result<Quote> Create(Text text, Author author, Category category, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail(QuoteErrors.QuoteDateOutOfRange);
        }

        var quote = new Quote(text, author, category, quoteDate);

        return Result.Ok(quote);
    }

    public Result<Quote> Update(Text text, Author author, Category category, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail(QuoteErrors.QuoteDateOutOfRange);
        }

        Text = text;
        AuthorId = author.Id;
        Author = author;
        CategoryId = category.Id;
        Category = category;
        QuoteDate = quoteDate;

        return Result.Ok(this);
    }

    private static bool IsValidQuoteDate(Date birthDate, Date? deathDate, Date quoteDate)
    {
        if (deathDate == null)
        {
            return birthDate.Value.Year + 10 <= quoteDate.Value.Year;
        }

        if (birthDate.Value.Year + 10 > quoteDate.Value.Year ||
            quoteDate.Value.Year >= deathDate.Value.Year)
        {
            return false;
        }

        return true;
    }
}