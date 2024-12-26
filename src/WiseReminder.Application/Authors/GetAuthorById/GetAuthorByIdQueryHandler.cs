namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorByIdQuery, Author>
{
    public async Task<Result<Author>> Handle(
        GetAuthorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetAuthorById(request.Id);

        return author == null ? Result.Fail(AuthorErrors.AuthorNotFound) : Result.Ok(author);
    }
}