namespace WiseReminder.Application.Quotes.DeleteQuoteByUser;

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
        var query = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(query, cancellationToken);

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

        await repository.DeleteQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}