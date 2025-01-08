namespace WiseReminder.Domain.Quotes;

public sealed class Quote : Entity<Quote>
{
    private Quote(Text text, Date quoteDate, Guid authorId, Guid categoryId)
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

    public static Result<Quote> CreateByAdmin(Text text, Date quoteDate, Author author,
        Guid categoryId)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return QuoteErrors.QuoteDateOutOfRange;
        }

        var quote = new Quote(text, quoteDate, author.Id, categoryId);

        return Result.Ok(quote);
    }

    public static Result<Quote> CreateByUser(Text text, Author author, Guid categoryId,
        Date quoteDate, Subscription subscription, int authorQuotesAmount)
    {
        if (!AuthorMayHaveAdditionalQuote(subscription, authorQuotesAmount))
        {
            return QuoteErrors.QuoteLimitExceeded;
        }

        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return QuoteErrors.QuoteDateOutOfRange;
        }

        var quote = new Quote(text, quoteDate, author.Id, categoryId);

        return Result.Ok(quote);
    }

    public Result Update(Text text, Author author, Guid categoryId, Date quoteDate)
    {
        if (!IsValidQuoteDate(author.BirthDate, author.DeathDate, quoteDate))
        {
            return QuoteErrors.QuoteDateOutOfRange;
        }

        Text = text;
        AuthorId = author.Id;
        CategoryId = categoryId;
        QuoteDate = quoteDate;

        return Result.Ok();
    }

    public Result BelongsToAuthor(Guid authorId)
    {
        if (AuthorId != authorId)
        {
            return QuoteErrors.QuoteNotBelongsToThisAuthor;
        }

        return Result.Ok();
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

    private static bool AuthorMayHaveAdditionalQuote(Subscription subscription,
        int authorQuotesAmount)
    {
        return subscription switch
        {
            Subscription.Free => authorQuotesAmount < 5,
            Subscription.Iron => authorQuotesAmount < 50,
            Subscription.Diamond => authorQuotesAmount < 1000,
            _ => throw new ArgumentOutOfRangeException(nameof(subscription), subscription, null)
        };
    }
}