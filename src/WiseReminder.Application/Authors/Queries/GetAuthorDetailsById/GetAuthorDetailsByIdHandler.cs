namespace WiseReminder.Application.Authors.Queries.GetAuthorDetailsById;

public sealed class GetAuthorDetailsByIdHandler(
    IAuthorRepository repository)
    : IQueryHandler<GetAuthorDetailsByIdQuery, AuthorDetails>
{
    public async Task<Result<AuthorDetails>> Handle(GetAuthorDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var authorDetails = await repository.GetAuthorDetailsById(request.Id, cancellationToken);

        if (authorDetails == null)
        {
            return AuthorErrors.AuthorNotFound;
        }

        return authorDetails;
    }
}