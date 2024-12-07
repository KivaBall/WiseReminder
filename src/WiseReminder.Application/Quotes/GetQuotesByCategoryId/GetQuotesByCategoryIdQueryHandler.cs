namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed class GetQuotesByCategoryIdQueryHandler(
    IQuoteRepository quoteRepository,
    ISender sender)
    : IQueryHandler<GetQuotesByCategoryIdQuery, ICollection<QuoteDto>>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly ISender _sender = sender;

    public async Task<Result<ICollection<QuoteDto>>> Handle(
        GetQuotesByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCategoryByIdQuery(request.CategoryId), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure<ICollection<QuoteDto>>(null, result.Error);
        }

        var quotes = await _quoteRepository.GetQuotesByCategoryId(request.CategoryId);

        var dtoQuotes = quotes.Select(q => q.ToQuoteDto()).ToList();

        return Result.Success<ICollection<QuoteDto>>(dtoQuotes);
    }
}