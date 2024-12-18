namespace WiseReminder.Application.Quotes.GetRandomQuote;

public sealed class GetRandomQuoteQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuoteQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetRandomQuoteQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetRandomQuotes(1);

        return Result.Ok(quote.First().ToQuoteDto());
    }
}