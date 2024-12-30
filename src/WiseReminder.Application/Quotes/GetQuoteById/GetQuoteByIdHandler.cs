namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, Quote>
{
    public async Task<Result<Quote>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetQuoteById(request.Id);

        if (quote == null)
        {
            return Result.Fail(QuoteErrors.QuoteNotFound);
        }

        return Result.Ok(quote);
    }
}