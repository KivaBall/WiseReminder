namespace WiseReminder.Application.Quotes.GetQuoteOfTheDay;

public sealed class GetQuoteOfTheDayHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteOfTheDay, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetQuoteOfTheDay request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetQuoteOfTheDay();

        return Result.Ok(quote.ToQuoteDto());
    }
}