namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDtos;

public sealed class GetRecentAddedQuoteDtosHandler(
    IQuoteRepository repository)
    : IQueryHandler<GetRecentAddedQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRecentAddedQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await repository.GetRecentAddedQuotes(request.Amount);

        ICollection<QuoteDto> quoteDtos = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}