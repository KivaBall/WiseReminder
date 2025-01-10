namespace WiseReminder.Application.Authors.Queries.GetAuthorDetailsByUserId;

public sealed class GetAuthorDetailsByUserIdHandler(
    IAuthorRepository repository,
    ISender sender)
    : IQueryHandler<GetAuthorDetailsByUserIdQuery, AuthorDetails>
{
    public async Task<Result<AuthorDetails>> Handle(GetAuthorDetailsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new HasUserByIdQuery(request.UserId);

        var userExists = await sender.Send(query, cancellationToken);

        if (userExists.IsFailed)
        {
            return userExists.ToResult();
        }

        var authorDetails =
            await repository.GetAuthorDetailsByUserId(request.UserId, cancellationToken);

        if (authorDetails == null)
        {
            return AuthorErrors.UserAuthorNotFound;
        }

        return authorDetails;
    }
}