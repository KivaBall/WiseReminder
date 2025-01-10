namespace WiseReminder.Application.Quotes.Commands.UpdateQuoteByUser;

public sealed class UpdateQuoteByUserHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteByUserCommand>
{
    public async Task<Result> Handle(
        UpdateQuoteByUserCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(quoteQuery, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var belongsToAuthor = quote.Value.IsAuthor(author.Value.Id);

        if (belongsToAuthor.IsFailed)
        {
            return belongsToAuthor;
        }

        var categoryQuery = new HasCategoryByIdQuery(request.CategoryId);

        var categoryExists = await sender.Send(categoryQuery, cancellationToken);

        if (categoryExists.IsFailed)
        {
            return categoryExists.ToResult();
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return quoteDate.ToResult();
        }

        var result = quote.Value.Update(text, author.Value, request.CategoryId, quoteDate.Value);

        if (result.IsFailed)
        {
            return result;
        }

        await repository.UpdateQuote(quote.Value, cancellationToken);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}