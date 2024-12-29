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

        authorRepository.DeleteAuthor(author.Value);

        var quotesQuery = new GetQuoteDtosByAuthorIdQuery { AuthorId = request.Id };

        var quotes = await sender.Send(quotesQuery, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        return await unitOfWork.SaveChangesAsync();
    }
}