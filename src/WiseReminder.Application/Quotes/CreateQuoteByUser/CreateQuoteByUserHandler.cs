namespace WiseReminder.Application.Quotes.CreateQuoteByUser;

public sealed class CreateQuoteByUserHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteByUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteByUserCommand request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery(request.UserId);

        var user = await sender.Send(userQuery, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
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

        var amountOfQuotes = await repository.GetNumberOfQuotesByAuthorId(author.Value.Id);

        var quote = Quote.CreateByUser(text, author.Value, category.Value.Id, quoteDate.Value,
            user.Value.Subscription, amountOfQuotes);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        repository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => quote.Value.Id);
    }
}