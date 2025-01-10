namespace WiseReminder.Application.Quotes.Queries.GetWeeklyQuoteDto;

public sealed class GetWeeklyQuoteDtoHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetWeeklyQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetWeeklyQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetWeeklyQuote(cancellationToken);

        if (request.DesiredLanguage == null)
        {
            return quote.ToQuoteDto(null);
        }

        var query = new GetTranslatedQuoteDtoQuery(quote, request.DesiredLanguage);

        var quoteDto = await sender.Send(query, cancellationToken);

        return quoteDto;
    }
}