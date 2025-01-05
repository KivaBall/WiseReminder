namespace WiseReminder.Application.Authors.UserDeleteAuthor;

public sealed class UserDeleteAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UserDeleteAuthorCommand>
{
    public async Task<Result> Handle(
        UserDeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var quotesQuery = new DeleteQuotesByAuthorIdCommand(author.Value.Id);

        var result = await sender.Send(quotesQuery, cancellationToken);

        if (result.IsFailed)
        {
            return result;
        }

        authorRepository.DeleteAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}