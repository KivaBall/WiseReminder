namespace WiseReminder.Application.Quotes.GetQuoteOfTheDay;

public sealed class GetQuoteOfTheDayHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteOfTheDay, QuoteDto>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteDto>> Handle(
        GetQuoteOfTheDay request,
        CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteOfTheDay();

        return Result.Success(quote.ToQuoteDto());
    }
}