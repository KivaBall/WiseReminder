namespace WiseReminder.Application.Quotes.GetQuoteDtosByUserId;

public sealed class GetQuoteDtosByUserIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetQuoteDtosByUserIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByUserIdQuery { Id = request.UserId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var quotesQuery = new GetQuoteDtosByAuthorIdQuery { AuthorId = author.Value.Id };

        var quotes = await sender.Send(quotesQuery, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        return Result.Ok(quotes.Value);
    }
}