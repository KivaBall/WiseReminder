namespace WiseReminder.Application.Quotes.GetDailyQuoteDto;

public sealed class GetDailyQuoteDtoHandler(
    IQuoteRepository repository)
    : IQueryHandler<GetDailyQuoteDtoQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetDailyQuoteDtoQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetDailyQuote(); //TODO: Maybe create service for this?

        var quoteDto = quote.ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}