namespace WiseReminder.Application.Abstractions.Mappers;

public static class QuoteMapper
{
    public static QuoteDto ToQuoteDto(this Quote quote)
    {
        return new QuoteDto
        {
            Id = quote.Id,
            Text = quote.Text.Value,
            QuoteDate = quote.QuoteDate.Value,
            AuthorId = quote.AuthorId,
            CategoryId = quote.CategoryId
        };
    }
}