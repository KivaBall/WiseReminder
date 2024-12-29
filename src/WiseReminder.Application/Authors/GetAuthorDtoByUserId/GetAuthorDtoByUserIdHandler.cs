namespace WiseReminder.Application.Authors.GetAuthorDtoByUserId;

public sealed class GetAuthorDtoByUserIdHandler(
    ISender sender)
    : IQueryHandler<GetAuthorDtoByUserIdQuery, AuthorDto>
{
    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var authorDto = author.Value.ToAuthorDto();

        return Result.Ok(authorDto);
    }
}