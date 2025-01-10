namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtoById;

public sealed class GetQuoteDtoByIdHandler(
    ISender sender,
    IQuoteRepository repository)
    : IQueryHandler<GetQuoteDtoByIdQuery, QuoteDto>
{
    public async Task<Result<QuoteDto>> Handle(
        GetQuoteDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var quote = await repository.GetQuoteDetailsById(request.Id, cancellationToken);

        if (quote == null)
        {
            return QuoteErrors.QuoteNotFound;
        }

        if (request.DesiredLanguage == null)
        {
            return quote.ToQuoteDto(null);
        }

        var translateQuery = new GetTranslatedQuoteDtoQuery(quote, request.DesiredLanguage);

        var quoteDto = await sender.Send(translateQuery, cancellationToken);

        return quoteDto;
    }
}