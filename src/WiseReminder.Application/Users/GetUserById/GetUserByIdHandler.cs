namespace WiseReminder.Application.Users.GetUserById;

public sealed class GetUserByIdHandler(
    IUserRepository repository)
    : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetUserById(request.Id);

        if (user == null)
        {
            return UserErrors.UserNotFound;
        }

        return Result.Ok(user);
    }
}