namespace WiseReminder.Application.Quotes.Queries.HasQuoteById;

public sealed class HasQuoteByIdHandler(
    IQuoteRepository repository)
    : IQueryHandler<HasQuoteByIdQuery, bool>
{
    public async Task<Result<bool>> Handle(
        HasQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quoteExists = await repository.HasQuoteById(request.Id, cancellationToken);

        if (!quoteExists)
        {
            return QuoteErrors.QuoteNotFound;
        }

        return Result.Ok(quoteExists);
    }
}