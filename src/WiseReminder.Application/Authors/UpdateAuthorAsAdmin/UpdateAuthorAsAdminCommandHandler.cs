namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed class UpdateAuthorAsAdminCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateAuthorAsAdminCommand>
{
    public async Task<Result> Handle(
        UpdateAuthorAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.Id };

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return Result.Fail(birthDate.Errors);
        }

        if (request.DeathDate == null)
        {
            author.Value.Update(name, biography, birthDate.Value, null);
        }
        else
        {
            var deathDate = Date.Create(request.DeathDate.Value);

            if (deathDate.IsFailed)
            {
                return Result.Fail(deathDate.Errors);
            }

            author.Value.Update(name, biography, birthDate.Value, deathDate.Value);
        }

        authorRepository.UpdateAuthor(author.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}