namespace WiseReminder.Application.Quotes.GetRecentAddedQuotes;

public sealed class GetRecentAddedQuotesHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRecentAddedQuotes, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRecentAddedQuotes request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRecentAddedQuotes(request.Amount);

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Ok<ICollection<QuoteDto>>(dtoQuotes);
    }
}