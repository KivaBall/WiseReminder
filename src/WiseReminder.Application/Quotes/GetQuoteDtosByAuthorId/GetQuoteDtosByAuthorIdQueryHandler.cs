namespace WiseReminder.Application.Quotes.GetQuoteDtosByAuthorId;

public sealed class GetQuoteDtosByAuthorIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuoteDtosByAuthorIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.AuthorId };

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var quotes = await quoteRepository.GetQuotesByAuthorId(request.AuthorId);

        ICollection<QuoteDto> quoteDtos = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}