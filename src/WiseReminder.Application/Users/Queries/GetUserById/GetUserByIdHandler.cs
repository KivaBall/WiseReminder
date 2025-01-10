namespace WiseReminder.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler(
    IUserRepository repository)
    : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetUserById(request.Id, cancellationToken);

        if (user == null)
        {
            return UserErrors.UserNotFound;
        }

        return Result.Ok(user);
    }
}