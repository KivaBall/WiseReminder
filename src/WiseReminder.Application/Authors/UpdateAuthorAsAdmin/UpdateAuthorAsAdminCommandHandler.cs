namespace WiseReminder.Application.Authors.UpdateAuthorAsAdmin;

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
            return author.ToResult();
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