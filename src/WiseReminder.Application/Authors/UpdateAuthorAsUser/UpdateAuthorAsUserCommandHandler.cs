namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed class UpdateAuthorAsUserCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateAuthorAsUserCommand>
{
    public async Task<Result> Handle(
        UpdateAuthorAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery { Id = request.UserId };

        var user = await sender.Send(userQuery);

        if (user.IsFailed)
        {
            return Result.Fail(user.Errors);
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }
        
        var authorQuery = new GetAuthorByIdQuery { Id = (Guid)user.Value.AuthorId };
        
        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
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