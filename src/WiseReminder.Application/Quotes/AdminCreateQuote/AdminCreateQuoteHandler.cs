namespace WiseReminder.Application.Quotes.AdminCreateQuote;

public sealed class AdminCreateQuoteHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<AdminCreateQuoteCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        AdminCreateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery(request.AuthorId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        var categoryQuery = new GetCategoryByIdQuery(request.CategoryId);

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return quoteDate.ToResult();
        }

        var quote = Quote.Create(text, author.Value, category.Value.Id, quoteDate.Value);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        quoteRepository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => quote.Value.Id);
    }
}