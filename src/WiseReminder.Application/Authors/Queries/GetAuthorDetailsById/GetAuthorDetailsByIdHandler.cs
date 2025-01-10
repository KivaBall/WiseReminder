namespace WiseReminder.Application.Authors.Queries.GetAuthorDetailsById;

public sealed class GetAuthorDetailsByIdHandler(
    IAuthorRepository repository,
    ISender sender)
    : IQueryHandler<GetAuthorDetailsByIdQuery, AuthorDetails>
{
    public async Task<Result<AuthorDetails>> Handle(GetAuthorDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new HasUserByIdQuery(request.Id);

        var userExists = await sender.Send(query, cancellationToken);

        if (userExists.IsFailed)
        {
            return userExists.ToResult();
        }

        var authorDetails = await repository.GetAuthorDetailsById(request.Id, cancellationToken);

        if (authorDetails == null)
        {
            return AuthorErrors.AuthorNotFound;
        }

        return authorDetails;
    }
}