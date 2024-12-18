namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed class GetQuotesByCategoryIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuotesByCategoryIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuotesByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery { Id = request.CategoryId };

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var quotes = await quoteRepository.GetQuotesByCategoryId(request.CategoryId);

        var dtoQuotes = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok<ICollection<QuoteDto>>(dtoQuotes);
    }
}