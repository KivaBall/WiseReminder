namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed class GetAuthorByUserIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetAuthorByUserIdQuery, Author>
{
    public async Task<Result<Author>> Handle(
        GetAuthorByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery { Id = request.Id };

        var user = await sender.Send(userQuery, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        if (user.Value.AuthorId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }

        var authorQuery = new GetAuthorByIdQuery { Id = user.Value.AuthorId.Value };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        return Result.Ok(author.Value);
    }
}