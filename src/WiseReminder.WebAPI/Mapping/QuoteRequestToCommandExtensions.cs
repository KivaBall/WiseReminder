namespace WiseReminder.WebAPI.Mapping;

public static class QuoteRequestToCommandExtensions
{
    public static CreateQuoteCommand ToCreateQuoteCommand(this BaseQuoteRequest request)
    {
        return new CreateQuoteCommand
        {
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }

    public static UpdateQuoteCommand ToUpdateQuoteCommand(this UpdateQuoteRequest request)
    {
        return new UpdateQuoteCommand
        {
            Id = request.Id,
            Text = request.Text,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId,
            QuoteDate = request.QuoteDate
        };
    }
}