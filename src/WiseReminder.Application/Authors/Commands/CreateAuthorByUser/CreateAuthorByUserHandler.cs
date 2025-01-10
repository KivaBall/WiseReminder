namespace WiseReminder.Application.Authors.Commands.CreateAuthorByUser;

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
        var query = new HasAuthorByUserIdQuery(request.UserId);

        var authorExists = await sender.Send(query, cancellationToken);

        if (authorExists.IsSuccess)
        {
            return AuthorErrors.DuplicateAuthorForUser;
        }

        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return birthDate.ToResult();
        }

        var author = Author.CreateByUser(name, biography, birthDate.Value, request.UserId);

        repository.CreateAuthor(author);

        return await unitOfWork.SaveChangesAsync(author.Id, cancellationToken);
    }
}