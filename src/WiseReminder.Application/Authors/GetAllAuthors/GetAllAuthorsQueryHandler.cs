namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed class GetAllAuthorsQueryHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAllAuthorsQuery, ICollection<AuthorDto>>
{
    public async Task<Result<ICollection<AuthorDto>>> Handle(
        GetAllAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await authorRepository.GetAllAuthors();

        var dtoAuthors = authors
            .Select(a => a.ToAuthorDto())
            .ToList();

        return Result.Ok<ICollection<AuthorDto>>(dtoAuthors);
    }
}