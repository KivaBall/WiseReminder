namespace WiseReminder.WebAPI.Mappers;

public static class UserRequestMapper
{
    public static RegisterUserCommand ToRegisterUserCommand(
        this UserRequest request)
    {
        return new RegisterUserCommand
        {
            Username = request.Username,
            Login = request.Login,
            Password = request.Password
        };
    }

    public static LoginAsAdminCommand ToLoginAsAdminCommand(
        this LoginAsAdminRequest request)
    {
        return new LoginAsAdminCommand
        {
            FirstPassword = request.FirstPassword,
            SecondPassword = request.SecondPassword,
            ThirdPassword = request.ThirdPassword
        };
    }

    public static LoginAsUserCommand ToLoginAsUserCommand(
        this LoginAsUserRequest asUserRequest)
    {
        return new LoginAsUserCommand
        {
            Login = asUserRequest.Login,
            Password = asUserRequest.Password
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

    public static UpdateUserCommand ToUpdateUserCommand(
        this UpdateUserRequest request,
        Guid userId)
    {
        return new UpdateUserCommand
        {
            Id = userId,
            OldPassword = request.OldPassword,
            NewUsername = request.NewUsername,
            NewPassword = request.NewPassword
        };
    }
}