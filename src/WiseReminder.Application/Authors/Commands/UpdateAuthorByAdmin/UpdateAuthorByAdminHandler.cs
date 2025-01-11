namespace WiseReminder.Application.Authors.Commands.UpdateAuthorByAdmin;

public sealed class UpdateAuthorByAdminHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateAuthorByAdminCommand>
{
    public async Task<Result> Handle(
        UpdateAuthorByAdminCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(request.Id);

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var hasAccess = author.Value.HasAccess(false);

        if (hasAccess.IsFailed)
        {
            return hasAccess;
        }

        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return birthDate.ToResult();
        }

        var deathDate = request.DeathDate != null
            ? Date.Create(request.DeathDate.Value)
            : null;

        if (deathDate != null && deathDate.IsFailed)
        {
            return deathDate.ToResult();
        }

        var (minQuoteDate, maxQuoteDate) =
            await repository.GetMinAndMaxQuoteDatesById(request.Id, cancellationToken);

        if (minQuoteDate == null || maxQuoteDate == null)
        {
            return AuthorErrors.AuthorQuoteDatesNotFound;
        }

        var result = author.Value.UpdateByAdmin(name, biography, birthDate.Value, deathDate?.Value,
            minQuoteDate, maxQuoteDate);

        if (result.IsFailed)
        {
            return result;
        }

        repository.UpdateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}