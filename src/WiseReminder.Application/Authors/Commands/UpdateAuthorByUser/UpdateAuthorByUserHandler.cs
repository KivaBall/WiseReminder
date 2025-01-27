namespace WiseReminder.Application.Authors.Commands.UpdateAuthorByUser;

public sealed class UpdateAuthorByUserHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateAuthorByUserCommand>
{
    public async Task<Result> Handle(
        UpdateAuthorByUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(query, cancellationToken);

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

        var (minQuoteDate, _) =
            await repository.GetMinAndMaxQuoteDatesById(author.Value.Id, cancellationToken);

        if (minQuoteDate == null)
        {
            return AuthorErrors.AuthorQuoteDatesNotFound;
        }

        var result = author.Value.UpdateByUser(name, biography, birthDate.Value, minQuoteDate);

        if (result.IsFailed)
        {
            return result;
        }

        repository.UpdateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}