namespace WiseReminder.Application.Quotes.Queries.GetDailyQuoteDto;

public sealed class GetDailyQuoteDtoHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetDailyQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetDailyQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetDailyQuote(cancellationToken);

        if (request.DesiredLanguage == null)
        {
            return quote.ToQuoteDto(null);
        }

        var query = new GetTranslatedQuoteDtoQuery(quote, request.DesiredLanguage);

        var quoteDto = await sender.Send(query, cancellationToken);

        return quoteDto;
    }
}