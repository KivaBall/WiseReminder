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
            return Result.Fail(user.Errors);
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }
        
        var authorQuery = new GetAuthorByIdQuery { Id = (Guid)user.Value.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        authorRepository.DeleteAuthor(author.Value);

        var quotesQuery = new GetQuotesByAuthorIdQuery { AuthorId = author.Value.Id };

        var quotesResult = await sender.Send(quotesQuery, cancellationToken);

        if (quotesResult.IsFailed)
        {
            return Result.Fail(quotesResult.Errors);
        }

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}