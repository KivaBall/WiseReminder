namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateAuthorCommand>
{
    public async Task<Result> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.Id };

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var author = result.Value;

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

        author.Update(name, biography, birthDate.Value, deathDate.Value);

        authorRepository.UpdateAuthor(author);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok();
    }
}