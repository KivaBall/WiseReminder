namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed class CreateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var name = new AuthorName(request.Name);

        var biography = new AuthorBiography(request.Biography);

        var birthDate = Date.Create((short)request.BirthDate.Year,
            (byte)request.BirthDate.Month, (byte)request.BirthDate.Day);

        if (birthDate.IsFailed)
        {
            return Result.Fail(birthDate.Errors);
        }

        var deathDate = request.DeathDate != null
            ? Date.Create((short)request.DeathDate?.Year!, (byte)request.DeathDate?.Month!,
                (byte)request.DeathDate?.Day!)
            : null;

        if (deathDate != null && deathDate.IsFailed)
        {
            return Result.Fail(deathDate.Errors);
        }

        var user = request.UserId != null
            ? 
        
        var result = Author.Create(name, biography, birthDate.Value, deathDate.Value);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        Author author = result.Value;

        authorRepository.CreateAuthor(author);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok(author.Id);
    }
}