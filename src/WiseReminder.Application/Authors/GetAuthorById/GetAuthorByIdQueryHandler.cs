namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorByIdQuery, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Result<Author>> Handle(
        GetAuthorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        return author != null
            ? Result.Success(author)
            : Result.Failure<Author>(null, AuthorErrors.AuthorNotFound);
    }
}