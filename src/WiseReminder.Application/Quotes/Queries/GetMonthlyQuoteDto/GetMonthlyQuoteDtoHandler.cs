namespace WiseReminder.Application.Quotes.Queries.GetMonthlyQuoteDto;

public sealed class GetMonthlyQuoteDtoHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetMonthlyQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetMonthlyQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetMonthlyQuote(cancellationToken);

        if (request.DesiredLanguage == null)
        {
            return quote.ToQuoteDto(null);
        }

        var query = new GetTranslatedQuoteDtoQuery(quote, request.DesiredLanguage);

        var quoteDto = await sender.Send(query, cancellationToken);

        return quoteDto;
    }
}