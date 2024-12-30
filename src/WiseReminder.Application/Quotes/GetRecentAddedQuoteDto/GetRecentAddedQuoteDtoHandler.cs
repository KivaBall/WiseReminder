namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDto;

public sealed class GetRecentAddedQuoteDtoHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRecentAddedQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetRecentAddedQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRecentAddedQuotes(1);

        var quoteDto = quotes.First().ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}