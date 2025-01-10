namespace WiseReminder.Application.Quotes.Queries.GetTranslatedQuoteDtos;

public sealed class GetTranslatedQuoteDtosHandler(
    ITranslationService translationService)
    : IQueryHandler<GetTranslatedQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetTranslatedQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        ICollection<QuoteDto> quoteDtos;

        var texts = request.Quotes
            .Select(t => t.Quote.Text.Value)
            .ToList();

        var translatedTexts = await translationService
            .TranslateAsync(texts, request.DesiredLanguage, cancellationToken);

        if (translatedTexts != null)
        {
            quoteDtos = request.Quotes
                .Select((transfer, idx) => transfer
                    .ToQuoteDto(translatedTexts[idx]))
                .ToList();

            return Result.Ok(quoteDtos);
        }

        quoteDtos = request.Quotes
            .Select(transfer => transfer
                .ToQuoteDto(null))
            .ToList();

        return Result.Ok(quoteDtos);
    }
}