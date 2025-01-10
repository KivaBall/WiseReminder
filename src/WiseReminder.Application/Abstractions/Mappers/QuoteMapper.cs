namespace WiseReminder.Application.Abstractions.Mappers;

public static class QuoteMapper
{
    public static QuoteDto ToQuoteDto(this QuoteDetails quoteDetails, string? translatedText)
    {
        return new QuoteDto
        {
            Id = quoteDetails.Quote.Id,
            OriginalText = quoteDetails.Quote.Text.Value,
            IsTranslated = translatedText != null,
            TranslatedText = translatedText,
            QuoteDate = quoteDetails.Quote.QuoteDate.Value,
            AuthorId = quoteDetails.Quote.AuthorId,
            CategoryId = quoteDetails.Quote.CategoryId,
            Reactions = new ReactionStat
            {
                Likes = quoteDetails.Likes,
                Dislikes = quoteDetails.Dislikes
            }
        };
    }
}