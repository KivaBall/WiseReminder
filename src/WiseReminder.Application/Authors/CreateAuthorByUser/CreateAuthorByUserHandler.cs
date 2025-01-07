namespace WiseReminder.Application.Authors.CreateAuthorByUser;

public sealed class CreateAuthorByUserHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateAuthorByUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorByUserCommand request,
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
            return AuthorErrors.DuplicateAuthorForUser;
        }

        var author = Author.CreateByUser(name, biography, birthDate.Value, request.UserId);

        repository.CreateAuthor(author);

        return await unitOfWork.SaveChangesAsyncWithResult(() => author.Id);
    }
}