namespace WiseReminder.Application.Quotes.DeleteQuoteAsUser;

public sealed class DeleteQuoteAsUserCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteAsUserCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery { Id = request.Id };

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery { Id = request.UserId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (quote.Value.AuthorId != author.Value.Id)
        {
            return Result.Fail(UserErrors.UserIdNotValid);
        }

        quoteRepository.DeleteQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}