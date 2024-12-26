namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed class GetQuotesByAuthorIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuotesByAuthorIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuotesByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.AuthorId };

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        var quotes = await quoteRepository.GetQuotesByAuthorId(request.AuthorId);

        var dtoQuotes = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok<ICollection<QuoteDto>>(dtoQuotes);
    }
}