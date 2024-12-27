namespace WiseReminder.Application.Authors.GetAuthorDtoByUserId;

public sealed class GetAuthorDtoByUserIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetAuthorDtoByUserIdQuery, AuthorDto>
{
    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery { Id = request.UserId };

        var user = await sender.Send(userQuery, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }

        var authorQuery = new GetAuthorByIdQuery { Id = (Guid)user.Value.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var authorDto = author.Value.ToAuthorDto();
        
        return Result.Ok(authorDto);
    }
}