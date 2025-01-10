namespace WiseReminder.Application.Authors.Queries.HasAuthorByUserId;

public sealed class HasAuthorByUserIdHandler(
    IAuthorRepository repository,
    ISender sender)
    : IQueryHandler<HasAuthorByUserIdQuery, bool>
{
    public async Task<Result<bool>> Handle(
        HasAuthorByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new HasUserByIdQuery(request.UserId);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var authorExists = await repository.HasAuthorByUserId(request.UserId, cancellationToken);

        if (!authorExists)
        {
            return AuthorErrors.UserAuthorNotFound;
        }

        return Result.Ok(true);
    }
}