namespace WiseReminder.Application.Authors.UpdateAuthorAsUser;

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
            return user.ToResult();
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }
        
        var authorQuery = new GetAuthorByIdQuery { Id = (Guid)user.Value.AuthorId };
        
        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

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

        author.Value.Update(name, biography, birthDate.Value, deathDate?.Value);

        authorRepository.UpdateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}