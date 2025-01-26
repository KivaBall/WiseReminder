namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class QuoteData
{
    public const string Text = "Text";
    public const string NewText = "NewText";

    public static readonly DateOnly QuoteDate = new DateOnly(2000, 01, 01);
    public static readonly DateOnly NewQuoteDate = new DateOnly(2001, 01, 01);

    public static QuoteByAdminRequest CreateQuoteByAdminRequest(Guid authorId, Guid categoryId)
    {
        return ToQuoteByAdminRequest(Text, authorId, categoryId, QuoteDate);
    }

    public static QuoteByAdminRequest UpdateQuoteByAdminRequest(Guid authorId, Guid categoryId)
    {
        return ToQuoteByAdminRequest(NewText, authorId, categoryId, NewQuoteDate);
    }

    public static QuoteByAdminRequest InvalidQuoteByAdminRequest()
    {
        return ToQuoteByAdminRequest(null!, Guid.Empty, Guid.Empty, default);
    }

    private static QuoteByAdminRequest ToQuoteByAdminRequest(string text, Guid authorId,
        Guid categoryId, DateOnly quoteDate)
    {
        return new QuoteByAdminRequest
        {
            Text = text,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = quoteDate
        };
    }

    public static QuoteByUserRequest CreateUserQuoteRequest(Guid categoryId)
    {
        return ToQuoteByUserRequest(Text, categoryId, QuoteDate);
    }

    public static QuoteByUserRequest UpdateUserQuoteRequest(Guid categoryId)
    {
        return ToQuoteByUserRequest(NewText, categoryId, NewQuoteDate);
    }

    public static QuoteByUserRequest InvalidQuoteByUserRequest()
    {
        return ToQuoteByUserRequest(null!, Guid.Empty, default);
    }

    private static QuoteByUserRequest ToQuoteByUserRequest(string text, Guid categoryId,
        DateOnly quoteDate)
    {
        return new QuoteByUserRequest
        {
            Text = text,
            CategoryId = categoryId,
            QuoteDate = quoteDate
        };
    }
}