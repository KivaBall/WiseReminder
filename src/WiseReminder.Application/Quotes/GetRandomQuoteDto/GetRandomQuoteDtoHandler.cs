namespace WiseReminder.Application.Quotes.GetRandomQuoteDto;

public sealed class GetRandomQuoteDtoHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRandomQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetRandomQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetRandomQuotes(1);

        var quoteDto = quote.First().ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}