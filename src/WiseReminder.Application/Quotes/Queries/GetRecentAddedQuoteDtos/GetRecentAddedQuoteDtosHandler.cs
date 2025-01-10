namespace WiseReminder.Application.Quotes.Queries.GetRecentAddedQuoteDtos;

public sealed class GetRecentAddedQuoteDtosHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetRecentAddedQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRecentAddedQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await repository.GetRecentAddedQuotes(request.Amount, cancellationToken);

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