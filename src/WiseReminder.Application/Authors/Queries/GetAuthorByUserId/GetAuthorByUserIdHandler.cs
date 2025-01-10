namespace WiseReminder.Application.Authors.Queries.GetAuthorByUserId;

public sealed class GetAuthorByUserIdHandler(
    IAuthorRepository repository,
    ISender sender)
    : IQueryHandler<GetAuthorByUserIdQuery, Author>
{
    public async Task<Result<Author>> Handle(
        GetAuthorByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new HasUserByIdQuery(request.UserId);

        var userExists = await sender.Send(query, cancellationToken);

        if (userExists.IsFailed)
        {
            return userExists.ToResult();
        }

        var author = await repository.GetAuthorByUserId(request.UserId, cancellationToken);

        if (author == null)
        {
            return AuthorErrors.UserAuthorNotFound;
        }

        return Result.Ok(author);
    }
}