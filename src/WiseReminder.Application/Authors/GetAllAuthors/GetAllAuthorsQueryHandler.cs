namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed class GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
    : IQueryHandler<GetAllAuthorsQuery, ICollection<AuthorDto>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Result<ICollection<AuthorDto>>> Handle(GetAllAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthors();

        var dtoAuthors = authors.Select(c => c.ToAuthorDto()).ToList();

        return Result.Success((ICollection<AuthorDto>)dtoAuthors);
    }
}