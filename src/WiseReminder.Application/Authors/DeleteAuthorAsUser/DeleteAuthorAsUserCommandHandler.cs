namespace WiseReminder.Application.Authors.DeleteAuthorAsUser;

public sealed class DeleteAuthorAsUserCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteAuthorAsUserCommand>
{
    public async Task<Result> Handle(
        DeleteAuthorAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery { Id = request.UserId };

        var user = await sender.Send(userQuery, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }

        var authorQuery = new GetAuthorByIdQuery { Id = (Guid)user.Value.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        authorRepository.DeleteAuthor(author.Value);

        var quotesQuery = new GetQuoteDtosByAuthorIdQuery { AuthorId = author.Value.Id };

        var quotes = await sender.Send(quotesQuery, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        return await unitOfWork.SaveChangesAsync();
    }
}