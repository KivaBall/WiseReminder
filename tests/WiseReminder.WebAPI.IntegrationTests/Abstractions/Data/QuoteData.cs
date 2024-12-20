namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class QuoteData
{
    public const string DefaultText = "DefaultText";
    public const string UpdatedText = "UpdatedText";

    public static readonly DateOnly DefaultQuoteDate = new(2000, 01, 01);
    public static readonly DateOnly UpdatedQuoteDate = new(2001, 01, 01);

    public static BaseQuoteRequest BaseQuoteRequest(Guid authorId, Guid categoryId)
    {
        return new BaseQuoteRequest
        {
            Text = DefaultText,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = DefaultQuoteDate
        };
    }

    public static BaseQuoteRequest NotValidBaseQuoteRequest(Guid authorId, Guid categoryId)
    {
        return new BaseQuoteRequest
        {
            Text = null!,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = default
        };
    }

    public static UpdateQuoteRequest UpdateQuoteRequest(Guid id, Guid authorId, Guid categoryId)
    {
        return new UpdateQuoteRequest
        {
            Id = id,
            Text = UpdatedText,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = UpdatedQuoteDate
        };
    }

    public static UpdateQuoteRequest NotValidUpdateQuoteRequest(Guid id, Guid authorId,
        Guid categoryId)
    {
        return new UpdateQuoteRequest
        {
            Id = id,
            Text = null!,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = default
        };
    }
}