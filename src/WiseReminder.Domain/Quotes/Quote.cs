namespace WiseReminder.Domain.Quotes;

public sealed class Quote : Entity<Quote>
{
    private Quote(QuoteText text, Author author, Category category, Date quoteDate)
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

    public QuoteText Text { get; private set; }
    public Date QuoteDate { get; private set; }

    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }

    public static Result<Quote> Create(QuoteText text, Author author, Category category,
        Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail("Invalid quote date");
        }

        var quote = new Quote(text, author, category, quoteDate);

        return Result.Ok(quote);
    }

    public Result<Quote> Update(QuoteText text, Author author, Category category, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return Result.Fail("Invalid quote date");
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
            return birthDate.Year < quoteDate.Year;
        }

        return birthDate.Year < quoteDate.Year && deathDate.Year > quoteDate.Year;
    }
}