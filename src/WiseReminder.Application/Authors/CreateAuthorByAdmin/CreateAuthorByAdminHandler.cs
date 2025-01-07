namespace WiseReminder.Application.Authors.CreateAuthorByAdmin;

public sealed class CreateAuthorByAdminHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorByAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorByAdminCommand request,
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

        var author = Author.CreateByAdmin(name, biography, birthDate.Value, deathDate?.Value);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        repository.CreateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => author.Value.Id);
    }
}