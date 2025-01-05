namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed class GetQuotesByCategoryIdHandler(
    IQuoteRepository repository,
    ISender sender)
    : IQueryHandler<GetQuotesByCategoryIdQuery, ICollection<Quote>>
{
    public async Task<Result<ICollection<Quote>>> Handle(
        GetQuotesByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(request.CategoryId);

        var category = await sender.Send(query, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var quotes = await repository.GetQuotesByCategoryId(request.CategoryId);

        return Result.Ok(quotes);
    }
}