namespace WiseReminder.Application.Quotes.Queries.GetRandomQuoteDtos;

public sealed class GetRandomQuoteDtosHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetRandomQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRandomQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await repository.GetRandomQuotes(request.Amount, cancellationToken);

        if (request.DesiredLanguage == null)
        {
            return quotes
                .Select(q => q.ToQuoteDto(null))
                .ToList();
        }

        var quoteDtosQuery = new GetTranslatedQuoteDtosQuery(quotes, request.DesiredLanguage);

        return await sender.Send(quoteDtosQuery, cancellationToken);
    }
}