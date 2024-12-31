namespace WiseReminder.Application.Quotes.GetQuoteDtosByAuthorId;

public sealed class GetQuoteDtosByAuthorIdHandler(
    ISender sender)
    : IQueryHandler<GetQuoteDtosByAuthorIdQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuotesByAuthorIdQuery(request.AuthorId);

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