namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtos;

public sealed class GetQuoteDtosHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetQuoteDtosQuery, ICollection<QuoteDto>>
{
    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuoteDtosQuery request,
        CancellationToken cancellationToken)
    {
        var quotes =
            await repository.GetQuoteDetailsByClauses(
                request.CategoryId,
                request.AuthorId,
                request.Keywords,
                cancellationToken);

        if (request.DesiredLanguage == null)
        {
            return quotes
                .Select(q => q.ToQuoteDto(null))
                .ToList();
        }

        var query = new GetTranslatedQuoteDtosQuery(quotes, request.DesiredLanguage);

        return await sender.Send(query, cancellationToken);
    }
}