namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed class GetAuthorDtosQueryHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorDtosQuery, ICollection<AuthorDto>>
{
    public async Task<Result<ICollection<AuthorDto>>> Handle(
        GetAuthorDtosQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await authorRepository.GetAllAuthors();

        var dtoAuthors = authors
            .Select(a => a.ToAuthorDto())
            .ToList();

        return Result.Ok<ICollection<AuthorDto>>(dtoAuthors);
    }
}