namespace WiseReminder.Application.Quotes.Commands.DeleteQuoteByUser;

public sealed class DeleteQuoteByUserHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteByUserCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteByUserCommand request,
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

        await repository.DeleteQuote(quote.Value, cancellationToken);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}