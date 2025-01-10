namespace WiseReminder.WebAPI.Mappers;

public static class QuoteRequestMapper
{
    public static CreateQuoteByAdminCommand ToCreateQuoteByAdminCommand(
        this QuoteByAdminRequest request)
    {
        return new CreateQuoteByAdminCommand
        {
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static CreateQuoteByUserCommand ToCreateQuoteByUserCommand(
        this QuoteByUserRequest request,
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

    public static UpdateQuoteByAdminCommand ToUpdateQuoteByAdminCommand(
        this QuoteByAdminRequest request,
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

    public static UpdateQuoteByUserCommand ToUpdateQuoteByUserCommand(
        this QuoteByUserRequest request,
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

    public static GetQuoteDtosQuery ToGetQuoteDtosQuery(
        this GetQuotesRequest request)
    {
        return new GetQuoteDtosQuery
        {
            CategoryId = request.CategoryId,
            AuthorId = request.AuthorId,
            Keywords = request.Keywords,
            DesiredLanguage = request.DesiredLanguage
        };
    }
}