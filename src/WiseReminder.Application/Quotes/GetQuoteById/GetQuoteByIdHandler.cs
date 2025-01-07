namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdHandler(
    IQuoteRepository repository)
    : IQueryHandler<GetQuoteByIdQuery, Quote>
{
    public async Task<Result<Quote>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetQuoteById(request.Id);

        if (quote == null)
        {
            return QuoteErrors.QuoteNotFound;
        }

        return Result.Ok(quote);
    }
}