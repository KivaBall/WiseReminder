namespace WiseReminder.Application.Users.Queries.HasUserById;

public sealed class HasUserByIdHandler(
    IUserRepository repository)
    : IQueryHandler<HasUserByIdQuery, bool>
{
    public async Task<Result<bool>> Handle(
        HasUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var userExists = await repository.HasUserById(request.Id, cancellationToken);

        if (!userExists)
        {
            return UserErrors.UserNotFound;
        }

        return Result.Ok(true);
    }
}