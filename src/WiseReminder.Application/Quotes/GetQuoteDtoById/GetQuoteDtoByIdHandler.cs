namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed class GetQuoteDtoByIdHandler(
    ISender sender)
    : IQueryHandler<GetQuoteDtoByIdQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetQuoteDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var quoteDto = quote.Value.ToQuoteDto();

        return Result.Ok(quoteDto);
    }
}