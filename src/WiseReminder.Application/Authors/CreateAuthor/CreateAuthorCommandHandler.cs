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

        var birthDate = Date.Create((short)request.DateOfBirth.Year,
            (byte)request.DateOfBirth.Month, (byte)request.DateOfBirth.Day);

        if (birthDate.IsFailed)
        {
            return Result.Fail(birthDate.Errors);
        }

        var deathDate = Date.Create((short)request.DateOfDeath.Year,
            (byte)request.DateOfDeath.Month, (byte)request.DateOfDeath.Day);

        if (deathDate.IsFailed)
        {
            return Result.Fail(deathDate.Errors);
        }

        var author = Author.Create(name, biography, birthDate.Value, deathDate.Value);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        authorRepository.CreateAuthor(author.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok(author.Value.Id);
    }
}