namespace WiseReminder.Application.Quotes.Queries.GetQuotes;

public sealed class GetQuotesHandler(
    IQuoteRepository repository)
    : IQueryHandler<GetQuotesQuery, ICollection<Quote>>
{
    public async Task<Result<ICollection<Quote>>> Handle(
        GetQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await repository.GetQuotesByClauses(
            request.CategoryId,
            request.AuthorId,
            request.Keywords,
            cancellationToken);

        return Result.Ok(quotes);
    }
}