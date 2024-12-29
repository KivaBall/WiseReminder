namespace WiseReminder.WebAPI.Mapping;

public static class QuoteRequestToCommandExtensions
{
    public static CreateQuoteAsAdminCommand ToCreateQuoteAsAdminCommand(
        this BaseQuoteAsAdminRequest request)
    {
        return new CreateQuoteAsAdminCommand
        {
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static CreateQuoteAsUserCommand ToCreateQuoteAsUserCommand(
        this BaseQuoteAsUserRequest request,
        Guid userId)
    {
        return new CreateQuoteAsUserCommand
        {
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }

    public static UpdateQuoteAsAdminCommand ToUpdateQuoteAsAdminCommand(
        this BaseQuoteAsAdminRequest request,
        Guid id)
    {
        return new UpdateQuoteAsAdminCommand
        {
            Id = id,
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static UpdateQuoteAsUserCommand ToUpdateQuoteAsUserCommand(
        this BaseQuoteAsUserRequest request,
        Guid id,
        Guid userId)
    {
        return new UpdateQuoteAsUserCommand
        {
            Id = id,
            Text = request.Text,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate,
            UserId = userId
        };
    }
}