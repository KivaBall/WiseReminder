namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtosByUserId;

public sealed class GetQuoteDtosByUserIdHandler(
    ISender sender)
    : IQueryHandler<GetQuoteDtosByUserIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var quotesQuery = new GetQuoteDtosQuery
        {
            AuthorId = author.Value.Id,
            CategoryId = null,
            Keywords = null,
            DesiredLanguage = null
        };

        var quotes = await sender.Send(quotesQuery, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        return Result.Ok(quotes.Value);
    }
}