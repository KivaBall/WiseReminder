namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, Quote>
{
    public async Task<Result<Quote>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetQuoteById(request.Id);

        return quote == null ? Result.Fail(QuoteErrors.QuoteNotFound) : Result.Ok(quote);
    }
}