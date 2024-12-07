namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed class GetAuthorDtoByIdQueryHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorDtoByIdQuery, AuthorDto>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        return author != null
            ? Result.Success(author.ToAuthorDto())
            : Result.Failure<AuthorDto>(null, AuthorErrors.AuthorNotFound);
    }
}