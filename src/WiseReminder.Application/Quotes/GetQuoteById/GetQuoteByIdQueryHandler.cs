namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuoteByIdQuery, QuoteDto>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteDto>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        if (quote == null)
        {
            return Result.Failure<QuoteDto>(null, QuoteErrors.QuoteNotFound);
        }

        return Result.Success(quote.ToQuoteDto());
    }
}