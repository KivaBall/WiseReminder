namespace WiseReminder.Application.Quotes.Queries.GetQuoteById;

public sealed class GetQuoteByIdHandler(
    IQuoteRepository repository)
    : IQueryHandler<GetQuoteByIdQuery, Quote>
{
    public async Task<Result<Quote>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetQuoteById(request.Id, cancellationToken);

        if (quote == null)
        {
            return QuoteErrors.QuoteNotFound;
        }

        return Result.Ok(quote);
    }
}