namespace WiseReminder.Application.Quotes.GetRandomQuoteDtos;

public sealed class GetRandomQuoteDtosQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRandomQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRandomQuotes(request.Amount);

        ICollection<QuoteDto> quoteDtos = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}