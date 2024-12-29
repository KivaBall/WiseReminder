namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDto;

public sealed class GetRecentAddedQuoteDtoHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetRecentAddedQuoteDto, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetRecentAddedQuoteDto request,
        CancellationToken cancellationToken)
    {
        var quotes = await quoteRepository.GetRecentAddedQuotes(1);

        var quoteDto = quotes.First().ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}