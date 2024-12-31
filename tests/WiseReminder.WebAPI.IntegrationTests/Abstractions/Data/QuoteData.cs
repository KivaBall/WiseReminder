namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class QuoteData
{
    public static string DefaultText = "DefaultText";
    public static readonly DateOnly DefaultQuoteDate = new(2000, 01, 01);

    public static string UpdatedText = "UpdatedText";
    public static readonly DateOnly UpdatedQuoteDate = new(2001, 01, 01);

    public static AdminQuoteRequest NotValidAdminQuoteRequest =>
        AdminQuoteRequest(null!, Guid.Empty, Guid.Empty, default);

    public static UserQuoteRequest NotValidUserQuoteRequest =>
        UserQuoteRequest(null!, Guid.Empty, default);

    private static AdminQuoteRequest AdminQuoteRequest(string text, Guid authorId, Guid categoryId,
        DateOnly quoteDate)
    {
        return new AdminQuoteRequest
        {
            Text = text,
            AuthorId = authorId,
            CategoryId = categoryId,
            QuoteDate = quoteDate
        };
    }

    public static AdminQuoteRequest CreateAdminQuoteRequest(Guid authorId, Guid categoryId)
    {
        return AdminQuoteRequest(DefaultText, authorId, categoryId, DefaultQuoteDate);
    }

    public static AdminQuoteRequest UpdateAdminQuoteRequest(Guid authorId, Guid categoryId)
    {
        return AdminQuoteRequest(UpdatedText, authorId, categoryId, UpdatedQuoteDate);
    }

    private static UserQuoteRequest UserQuoteRequest(string text, Guid categoryId,
        DateOnly quoteDate)
    {
        return new UserQuoteRequest
        {
            Text = text,
            CategoryId = categoryId,
            QuoteDate = quoteDate
        };
    }

    public static UserQuoteRequest CreateUserQuoteRequest(Guid categoryId)
    {
        return UserQuoteRequest(DefaultText, categoryId, DefaultQuoteDate);
    }

    public static UserQuoteRequest UpdateUserQuoteRequest(Guid categoryId)
    {
        return UserQuoteRequest(UpdatedText, categoryId, UpdatedQuoteDate);
    }
}