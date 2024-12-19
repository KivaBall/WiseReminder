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

        Author author;

        if (request.DeathDate == null)
        {
            var result = Author.Create(name, biography, birthDate.Value, null);

            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            authorRepository.CreateAuthor(result.Value);

            author = result.Value;
        }
        else
        {
            var deathDate = Date.Create((short)request.DeathDate?.Year!,
                (byte)request.DeathDate?.Month!, (byte)request.DeathDate?.Day!);

            if (deathDate.IsFailed)
            {
                return Result.Fail(deathDate.Errors);
            }

            var result = Author.Create(name, biography, birthDate.Value, deathDate.Value);

            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }

            authorRepository.CreateAuthor(result.Value);

            author = result.Value;
        }

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok(author.Id);
    }
}