namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, Quote>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<Quote>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        return quote != null
            ? Result.Success(quote)
            : Result.Failure<Quote>(null, QuoteErrors.QuoteNotFound);
    }
}