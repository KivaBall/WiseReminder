namespace WiseReminder.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.Id);

        return user == null ? Result.Fail(UserErrors.UserNotFound) : Result.Ok(user);
    }
}