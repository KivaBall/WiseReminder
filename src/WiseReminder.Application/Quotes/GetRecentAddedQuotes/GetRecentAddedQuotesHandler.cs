namespace WiseReminder.Application.Quotes.GetRecentAddedQuotes;

public sealed class GetRecentAddedQuotesHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRecentAddedQuotes, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRecentAddedQuotes request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetRecentAddedQuotes(request.Amount);

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}