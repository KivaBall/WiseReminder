namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed class GetQuotesByCategoryIdQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuotesByCategoryIdQuery, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteDto>>> Handle(GetQuotesByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetQuotesByCategoryId(request.CategoryId);

        if (quotes == null)
        {
            return Result.Failure<ICollection<QuoteDto>>(null, CategoryErrors.CategoryNotFound);
        }

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}