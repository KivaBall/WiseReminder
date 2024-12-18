namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed class GetAuthorDtoByIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetAuthorDtoByIdQuery, AuthorDto>
{
    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.Id };

        var result = await sender.Send(query);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        return Result.Ok(result.Value.ToAuthorDto());
    }
}