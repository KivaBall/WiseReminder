namespace WiseReminder.Application.Authors.AdminDeleteAuthor;

public sealed class AdminDeleteAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<AdminDeleteAuthorCommand>
{
    public async Task<Result> Handle(
        AdminDeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery(request.Id);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        var quotesQuery = new DeleteQuotesByAuthorIdCommand(request.Id);

        var result = await sender.Send(quotesQuery, cancellationToken);

        if (result.IsFailed)
        {
            return result;
        }

        authorRepository.DeleteAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}