namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDtos;

public sealed class GetRecentAddedQuoteDtosHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRecentAddedQuoteDtos, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRecentAddedQuoteDtos request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRecentAddedQuotes(request.Amount);

        ICollection<QuoteDto> quoteDtos = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}