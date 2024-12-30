namespace WiseReminder.Application.Users.GetUserById;

public sealed class GetUserByIdHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.Id);

        if (user == null)
        {
            return Result.Fail(UserErrors.UserNotFound);
        }

        return Result.Ok(user);
    }
}