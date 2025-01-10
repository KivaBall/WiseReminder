namespace WiseReminder.Application.Authors.Queries.GetAuthorDtoById;

public sealed class GetAuthorDtoByIdHandler(
    ISender sender)
    : IQueryHandler<GetAuthorDtoByIdQuery, AuthorDto>
{
    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorDetailsByIdQuery(request.Id);

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var authorDto = author.Value.ToAuthorDto();

        return Result.Ok(authorDto);
    }
}