namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed class GetQuotesByAuthorIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuotesByAuthorIdQuery, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly ISender _sender = sender;

    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuotesByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAuthorByIdQuery(request.AuthorId), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure<ICollection<QuoteDto>>(null, result.Error);
        }

        var quotes = await _quoteRepository.GetQuotesByAuthorId(request.AuthorId);

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}