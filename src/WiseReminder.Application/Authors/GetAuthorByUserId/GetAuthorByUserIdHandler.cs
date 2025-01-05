namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed class GetAuthorByUserIdHandler(
    IAuthorRepository repository,
    ISender sender)
    : IQueryHandler<GetAuthorByUserIdQuery, Author>
{
    public async Task<Result<Author>> Handle(
        GetAuthorByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userQuery = new GetUserByIdQuery(request.UserId);

        var user = await sender.Send(userQuery, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var author = await repository.GetAuthorByUserId(request.UserId);

        if (author == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }

        return Result.Ok(author);
    }
}