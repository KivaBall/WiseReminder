namespace WiseReminder.WebAPI.Mapping;

public static class UserRequestToCommandExtensions
{
    public static CreateUserCommand ToCreateUserCommand(
        this BaseUserRequest request)
    {
        return new CreateUserCommand
        {
            Username = request.Username,
            Login = request.Login,
            Password = request.Password
        };
    }

    public static ChangeUsernameCommand ToChangeUsernameCommand(
        this UpdateUsernameRequest request,
        Guid userId)
    {
        return new ChangeUsernameCommand
        {
            Id = userId,
            NewUsername = request.NewUsername,
            Password = request.Password
        };
    }

    public static ChangePasswordCommand ToChangePasswordCommand(
        this UpdatePasswordRequest request,
        Guid userId)
    {
        return new ChangePasswordCommand
        {
            Id = userId,
            OldPassword = request.OldPassword,
            NewPassword = request.NewPassword
        };
    }
}