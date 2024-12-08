namespace WiseReminder.Application.Quotes.GetRandomQuotes;

public sealed class GetRandomQuotesQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuotesQuery, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetRandomQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetRandomQuotes(request.Amount);

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}