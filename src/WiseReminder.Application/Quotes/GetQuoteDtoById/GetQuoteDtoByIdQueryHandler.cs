namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed class GetQuoteDtoByIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetQuoteDtoByIdQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetQuoteDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery { Id = request.Id };

        var result = await sender.Send(query);

        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok(result.Value.ToQuoteDto());
    }
}