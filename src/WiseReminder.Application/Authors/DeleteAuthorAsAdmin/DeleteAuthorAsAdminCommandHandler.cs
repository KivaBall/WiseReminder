namespace WiseReminder.Application.Authors.DeleteAuthorAsAdmin;

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