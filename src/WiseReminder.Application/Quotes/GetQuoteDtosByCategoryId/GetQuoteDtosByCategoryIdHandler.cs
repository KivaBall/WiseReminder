namespace WiseReminder.Application.Quotes.GetQuoteDtosByCategoryId;

public sealed class GetQuoteDtosByCategoryIdHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuoteDtosByCategoryIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuotesByCategoryIdQuery(request.CategoryId);

        var quotes = await sender.Send(query, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        ICollection<QuoteDto> quoteDtos = quotes.Value
            .Select(q => q.ToQuoteDto())
            .ToList();

        return Result.Ok(quoteDtos);
    }
}