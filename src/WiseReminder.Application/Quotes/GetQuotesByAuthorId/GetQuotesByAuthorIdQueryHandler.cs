namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed class GetQuotesByAuthorIdQueryHandler(IQuoteRepository quoteRepository)
    : IQueryHandler<GetQuotesByAuthorIdQuery, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteDto>>> Handle(GetQuotesByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetQuotesByAuthorId(request.AuthorId);

        if (quotes == null)
        {
            return Result.Failure<ICollection<QuoteDto>>(null, AuthorErrors.AuthorNotFound);
        }

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}