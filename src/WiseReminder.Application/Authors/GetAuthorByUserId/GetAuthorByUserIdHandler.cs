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
        var query = new GetUserByIdQuery(request.UserId);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var author = await repository.GetAuthorByUserId(request.UserId);

        if (author == null)
        {
            return AuthorErrors.UserAuthorNotFound;
        }

        return Result.Ok(author);
    }
}