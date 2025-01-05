namespace WiseReminder.Application.Authors.UserCreateAuthor;

public sealed class UserCreateAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UserCreateAuthorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        UserCreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return birthDate.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var result = await sender.Send(authorQuery, cancellationToken);

        if (result.IsSuccess)
        {
            return Result.Fail(AuthorErrors.AuthorExistsForUser);
        }

        var author = Author.Create(name, biography, birthDate.Value, null, request.UserId);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        authorRepository.CreateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => author.Value.Id);
    }
}