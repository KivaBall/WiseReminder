namespace WiseReminder.Application.Quotes.GetRandomQuote;

public sealed class GetRandomQuoteQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuoteQuery, QuoteDto>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteDto>> Handle(
        GetRandomQuoteQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetRandomQuote();

        return Result.Success(quote.ToQuoteDto());
    }
}