namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        if (author == null)
        {
            return Result.Failure<AuthorDto>(null, AuthorErrors.AuthorNotFound);
        }

        return Result.Success(author.ToAuthorDto());
    }
}