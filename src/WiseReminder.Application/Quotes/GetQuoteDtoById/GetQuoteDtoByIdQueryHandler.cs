namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed class GetQuoteDtoByIdQueryHandler(
    IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteDtoByIdQuery, QuoteDto>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteDto>> Handle(
        GetQuoteDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        return quote != null
            ? Result.Success(quote.ToQuoteDto())
            : Result.Failure<QuoteDto>(null, QuoteErrors.QuoteNotFound);
    }
}