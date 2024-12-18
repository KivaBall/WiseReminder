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

        if (author == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotFound);
        }

        return Result.Ok(author);
    }
}