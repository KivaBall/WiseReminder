namespace WiseReminder.Application.Authors.UserUpdateAuthor;

public sealed class UserUpdateAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UserUpdateAuthorCommand>
{
    public async Task<Result> Handle(
        UserUpdateAuthorCommand request,
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

        author.Value.Update(name, biography, birthDate.Value, null);

        authorRepository.UpdateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}