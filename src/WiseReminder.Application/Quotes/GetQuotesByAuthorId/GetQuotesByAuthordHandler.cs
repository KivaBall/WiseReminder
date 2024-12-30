namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed class GetQuotesByAuthorIdHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetQuotesByAuthorIdQuery, ICollection<Quote>>
{
    public async Task<Result<ICollection<Quote>>> Handle(
        GetQuotesByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(request.AuthorId);

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }
        
        var quotes = await repository.GetQuotesByAuthorId(request.AuthorId);
        
        return Result.Ok(quotes);
    }
}