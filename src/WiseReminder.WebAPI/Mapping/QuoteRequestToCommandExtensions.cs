namespace WiseReminder.WebAPI.Mapping;

public static class QuoteRequestToCommandExtensions
{
    public static CreateQuoteCommand ToCreateQuoteCommand(this BaseQuoteRequest request)
    {
        if (request.Text == null
            || request.AuthorId == null
            || request.CategoryId == null
            || request.QuoteDate == null)
        {
            throw new ArgumentNullException($"{nameof(BaseQuoteRequest)} has null property");
        }

        return new CreateQuoteCommand
        {
            Text = request.Text,
            AuthorId = (Guid)request.AuthorId,
            CategoryId = (Guid)request.CategoryId,
            QuoteDate = (DateOnly)request.QuoteDate
        };
    }

    public static UpdateQuoteCommand ToUpdateQuoteCommand(this UpdateQuoteRequest request)
    {
        if (request.Id == null
            || request.Text == null
            || request.AuthorId == null
            || request.CategoryId == null
            || request.QuoteDate == null)
        {
            throw new ArgumentNullException($"{nameof(UpdateQuoteRequest)} has null property");
        }

        return new UpdateQuoteCommand
        {
            Id = (Guid)request.Id,
            Text = request.Text,
            AuthorId = (Guid)request.AuthorId,
            CategoryId = (Guid)request.CategoryId,
            QuoteDate = (DateOnly)request.QuoteDate
        };
    }
}