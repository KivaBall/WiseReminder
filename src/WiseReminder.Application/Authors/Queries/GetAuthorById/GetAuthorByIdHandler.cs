namespace WiseReminder.Application.Authors.Queries.GetAuthorById;

public sealed class GetAuthorByIdHandler(
    IAuthorRepository repository)
    : IQueryHandler<GetAuthorByIdQuery, Author>
{
    public async Task<Result<Author>> Handle(
        GetAuthorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var author = await repository.GetAuthorById(request.Id, cancellationToken);

        if (author == null)
        {
            return AuthorErrors.AuthorNotFound;
        }

        return Result.Ok(author);
    }
}