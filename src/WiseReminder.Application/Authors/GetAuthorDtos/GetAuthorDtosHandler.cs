namespace WiseReminder.Application.Authors.GetAuthorDtos;

public sealed class GetAuthorDtosHandler(
    IAuthorRepository repository)
    : IQueryHandler<GetAuthorDtosQuery, ICollection<AuthorDto>>
{
    public async Task<Result<ICollection<AuthorDto>>> Handle(
        GetAuthorDtosQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await repository.GetAllAuthors();

        ICollection<AuthorDto> authorDtos = authors
            .Select(a => a.ToAuthorDto())
            .ToList();

        return Result.Ok(authorDtos);
    }
}