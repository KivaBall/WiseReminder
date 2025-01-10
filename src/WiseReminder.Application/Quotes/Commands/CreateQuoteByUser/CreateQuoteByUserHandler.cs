namespace WiseReminder.Application.Quotes.Commands.CreateQuoteByUser;

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

        var quotesAmount =
            await repository.GetQuotesAmountByAuthorId(author.Value.Id, cancellationToken);

        var quote = Quote.CreateByUser(text, author.Value, request.CategoryId, quoteDate.Value,
            user.Value.Subscription, quotesAmount);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        repository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync(quote.Value.Id, cancellationToken);
    }
}