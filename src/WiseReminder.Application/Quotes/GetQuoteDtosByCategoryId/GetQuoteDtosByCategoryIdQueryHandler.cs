namespace WiseReminder.Application.Quotes.GetQuoteDtosByCategoryId;

public sealed class GetQuoteDtosByCategoryIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuoteDtosByCategoryIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery { Id = request.CategoryId };

        var category = await sender.Send(query, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var quotes = await quoteRepository.GetQuotesByCategoryId(request.CategoryId);

        ICollection<QuoteDto> quoteDtos = quotes
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}