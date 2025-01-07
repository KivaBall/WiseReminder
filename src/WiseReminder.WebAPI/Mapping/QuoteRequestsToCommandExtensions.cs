namespace WiseReminder.WebAPI.Mapping;

public static class QuoteRequestsToCommandExtensions
{
    public static CreateQuoteByAdminCommand ToAdminCreateQuoteCommand(
        this AdminQuoteRequest request)
    {
        return new CreateQuoteByAdminCommand
        {
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static CreateQuoteByUserCommand ToUserCreateQuoteCommand(
        this UserQuoteRequest request,
        Guid userId)
    {
        return new CreateQuoteByUserCommand
        {
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }

    public static UpdateQuoteByAdminCommand ToAdminUpdateQuoteCommand(
        this AdminQuoteRequest request,
        Guid id)
    {
        return new UpdateQuoteByAdminCommand
        {
            Id = id,
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static UpdateQuoteByUserCommand ToUserUpdateQuoteCommand(
        this UserQuoteRequest request,
        Guid id,
        Guid userId)
    {
        return new UpdateQuoteByUserCommand
        {
            Id = id,
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }
}