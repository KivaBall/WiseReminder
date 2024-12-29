using WiseReminder.WebAPI.Controllers.Quotes;

namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class QuoteData
{
    public const string DefaultText = "DefaultText";
    public const string UpdatedText = "UpdatedText";

    public static readonly DateOnly DefaultQuoteDate = new(2000, 01, 01);
    public static readonly DateOnly UpdatedQuoteDate = new(2001, 01, 01);

    public static BaseQuoteAsAdminRequest BaseQuoteRequest(Guid authorId, Guid categoryId)
    {
        return new BaseQuoteAsAdminRequest
        {
            Text = DefaultText,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = DefaultQuoteDate
        };
    }

    public static BaseQuoteAsAdminRequest NotValidBaseQuoteRequest(Guid authorId, Guid categoryId)
    {
        return new BaseQuoteAsAdminRequest
        {
            Text = null!,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = default
        };
    }

    public static BaseQuoteAsAdminRequest UpdateQuoteRequest(Guid id, Guid authorId, Guid categoryId)
    {
        return new BaseQuoteAsAdminRequest
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