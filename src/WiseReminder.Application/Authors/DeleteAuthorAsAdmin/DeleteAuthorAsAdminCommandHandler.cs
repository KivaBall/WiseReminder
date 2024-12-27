namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed class DeleteAuthorAsAdminCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteAuthorAsAdminCommand>
{
    public async Task<Result> Handle(
        DeleteAuthorAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery { Id = request.Id };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        authorRepository.DeleteAuthor(author.Value);

        var quotesQuery = new GetQuotesByAuthorIdQuery { AuthorId = request.Id };

        var quotesResult = await sender.Send(quotesQuery, cancellationToken);

        if (quotesResult.IsFailed)
        {
            return Result.Fail(quotesResult.Errors);
        }

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}