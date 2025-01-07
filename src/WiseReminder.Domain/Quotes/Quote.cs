namespace WiseReminder.Domain.Quotes;

public sealed class Quote : Entity<Quote>
{
    private Quote(Text text, Guid authorId, Guid categoryId, Date quoteDate)
    {
        Text = text;
        AuthorId = authorId;
        CategoryId = categoryId;
        QuoteDate = quoteDate;
    }

    public Text Text { get; private set; }
    public Date QuoteDate { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; private set; }

    public static Result<Quote> Create(Text text, Author author, Guid categoryId, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail(QuoteErrors.QuoteDateOutOfRange);
        }

        var quote = new Quote(text, author.Id, categoryId, quoteDate);

        return Result.Ok(quote);
    }

    public Result<Quote> Update(Text text, Author author, Guid categoryId, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail(QuoteErrors.QuoteDateOutOfRange);
        }

        Text = text;
        AuthorId = author.Id;
        CategoryId = categoryId;
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