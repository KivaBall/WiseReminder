namespace WiseReminder.WebAPI.Mapping;

public static class UserRequestsToCommandExtensions
{
    public static RegisterUserCommand ToCreateUserCommand(
        this UserRequest request)
    {
        return new RegisterUserCommand
        {
            Username = request.Username,
            Login = request.Login,
            Password = request.Password
        };
    }

    public static LoginAsUserCommand ToUserLoginCommand(
        this UserLoginRequest request)
    {
        return new LoginAsUserCommand
        {
            Login = request.Login,
            Password = request.Password
        };
    }

    public static LoginAsAdminCommand ToAdminLoginCommand(
        this AdminLoginRequest request)
    {
        return new LoginAsAdminCommand
        {
            FirstPassword = request.FirstPassword,
            SecondPassword = request.SecondPassword,
            ThirdPassword = request.ThirdPassword
        };
    }

    public static ApplySubscriptionCommand ToApplySubscriptionCommand(
        this ApplySubscriptionRequest request, Guid userId)
    {
        return new ApplySubscriptionCommand
        {
            UserId = userId,
            Subscription = request.Subscription
        };
    }

    public static ChangeUsernameCommand ToChangeUsernameCommand(
        this ChangeUsernameRequest request,
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
        this ChangePasswordRequest request,
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