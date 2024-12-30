namespace WiseReminder.WebAPI.Mapping;

public static class QuoteRequestsToCommandExtensions
{
    public static AdminCreateQuoteCommand ToAdminCreateQuoteCommand(
        this AdminQuoteRequest request)
    {
        return new AdminCreateQuoteCommand
        {
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static UserCreateQuoteCommand ToUserCreateQuoteCommand(
        this UserQuoteRequest request,
        Guid userId)
    {
        return new UserCreateQuoteCommand
        {
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }

    public static AdminUpdateQuoteCommand ToAdminUpdateQuoteCommand(
        this AdminQuoteRequest request,
        Guid id)
    {
        return new AdminUpdateQuoteCommand
        {
            Id = id,
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static UserUpdateQuoteCommand ToUserUpdateQuoteCommand(
        this UserQuoteRequest request,
        Guid id,
        Guid userId)
    {
        return new UserUpdateQuoteCommand
        {
            Id = id,
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }
}