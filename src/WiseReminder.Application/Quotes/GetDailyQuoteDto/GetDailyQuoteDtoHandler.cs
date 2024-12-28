namespace WiseReminder.Application.Quotes.GetDailyQuoteDto;

public sealed class GetDailyQuoteDtoHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetDailyQuoteDto, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetDailyQuoteDto request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetQuoteOfTheDay(); //TODO: Maybe create service for this?

        var quoteDto = quote.ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}