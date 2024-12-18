namespace WiseReminder.Application.Quotes.GetRandomQuotes;

public sealed class GetRandomQuotesQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuotesQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRandomQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRandomQuotes(request.Amount);

        var dtoQuotes = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok<ICollection<QuoteDto>>(dtoQuotes);
    }
}