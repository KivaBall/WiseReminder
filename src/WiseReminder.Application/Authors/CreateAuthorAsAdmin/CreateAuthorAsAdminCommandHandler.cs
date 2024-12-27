namespace WiseReminder.Application.Authors.CreateAuthorAsAdmin;

public sealed class CreateAuthorAsAdminCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorAsAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return birthDate.ToResult();
        }

        var deathDate = request.DeathDate != null ? Date.Create(request.DeathDate.Value) : null;

        if (deathDate != null && deathDate.IsFailed)
        {
            return deathDate.ToResult();
        }

        var author = Author.Create(name, biography, birthDate.Value, deathDate?.Value, null);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        authorRepository.CreateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => author.Value.Id);
    }
}