namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed class CreateAuthorAsUserCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateAuthorAsUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return Result.Fail(birthDate.Errors);
        }

        var deathDate = request.DeathDate != null
            ? Date.Create(request.DeathDate.Value)
            : null;

        if (deathDate != null && deathDate.IsFailed)
        {
            return Result.Fail(deathDate.Errors);
        }

        var query = new GetUserByIdQuery { Id = request.UserId };
        
        var user = await sender.Send(query);

        if (user.IsFailed)
        {
            return Result.Fail(user.Errors);
        }

        if (user.Value.AuthorId != null)
        {
            return Result.Fail(AuthorErrors.AuthorExistsForUser);
        }
        
        var author = Author.Create(name, biography, birthDate.Value, deathDate?.Value, user.Value);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        authorRepository.CreateAuthor(author.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok(author.Value.Id);
    }
}