namespace WiseReminder.Application.Quotes.UpdateQuoteByUser;

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

        if (quote.Value.AuthorId != author.Value.Id)
        {
            return UserErrors.UserIdNotValid;
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

        var result = quote.Value.Update(text, author.Value, category.Value.Id, quoteDate.Value);

        if (result.IsFailed)
        {
            return result;
        }

        await repository.UpdateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}