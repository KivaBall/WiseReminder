namespace WiseReminder.Application.Quotes.Queries.GetTranslatedQuoteDto;

public sealed class GetTranslatedQuoteDtoHandler(
    ITranslationService translationService)
    : IQueryHandler<GetTranslatedQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetTranslatedQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var text = request.Quote.Quote.Text.Value;

        var translatedText = await translationService
            .TranslateAsync(text, request.DesiredLanguage, cancellationToken);

        if (translatedText != null)
        {
            var quoteDto = request.Quote.ToQuoteDto(translatedText);

            return Result.Ok(quoteDto);
        }

        var untranslatedQuoteDto = request.Quote.ToQuoteDto(null);

        return Result.Ok(untranslatedQuoteDto);
    }
}