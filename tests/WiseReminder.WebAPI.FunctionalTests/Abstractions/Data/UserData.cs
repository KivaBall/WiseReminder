namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class UserData
{
    public const string Username = "Username";
    public const string NewUsername = "Username";

    public const string EmptyUserLogin = "EmptyUserLogin";
    public const string UserWithDataLogin = "UserWithDataLogin";

    public const string Password = "Password";
    public const string NewPassword = "NewPassword";

    public static UserRequest CreateEmptyUserRequest()
    {
        return UserRequest(Username, EmptyUserLogin, Password);
    }

    public static UserRequest CreateUserWithDataRequest()
    {
        return UserRequest(Username, UserWithDataLogin, Password);
    }

    public static UserRequest InvalidUserRequest()
    {
        return UserRequest(null!, null!, null!);
    }

    private static UserRequest UserRequest(string username, string login, string password)
    {
        return new UserRequest
        {
            Username = username,
            Login = login,
            Password = password
        };
    }

    public static UpdateUserRequest UpdateUserRequest()
    {
        return ToUpdateUserRequest(Password, NewUsername, NewPassword);
    }

    public static UpdateUserRequest InvalidUpdateUserRequest()
    {
        return ToUpdateUserRequest(null!, null!, null!);
    }

    private static UpdateUserRequest ToUpdateUserRequest(string oldPassword, string newUsername,
        string newPassword)
    {
        return new UpdateUserRequest
        {
            OldPassword = oldPassword,
            NewUsername = newUsername,
            NewPassword = newPassword
        };
    }

    public static LoginAsUserRequest EmptyUserLoginRequest()
    {
        return ToLoginAsUserRequest(EmptyUserLogin, Password);
    }

    public static LoginAsUserRequest UserWithDataLoginRequest()
    {
        return ToLoginAsUserRequest(UserWithDataLogin, Password);
    }

    public static LoginAsUserRequest InvalidLoginAsUserRequest()
    {
        return ToLoginAsUserRequest(null!, null!);
    }

    private static LoginAsUserRequest ToLoginAsUserRequest(string login, string password)
    {
        return new LoginAsUserRequest
        {
            Login = login,
            Password = password
        };
    }

    public static LoginAsAdminRequest LoginAsAdminRequest()
    {
        return ToLoginAsAdminRequest("first_secret_admin_password", "second_secret_admin_password",
            "third_secret_admin_password");
    }

    public static LoginAsAdminRequest InvalidLoginAsAdminRequest()
    {
        return ToLoginAsAdminRequest(null!, null!, null!);
    }

    private static LoginAsAdminRequest ToLoginAsAdminRequest(string firstPassword,
        string secondPassword, string thirdPassword)
    {
        return new LoginAsAdminRequest
        {
            FirstPassword = firstPassword,
            SecondPassword = secondPassword,
            ThirdPassword = thirdPassword
        };
    }

    public static ApplySubscriptionRequest ApplySubscriptionRequest()
    {
        return ToApplySubscriptionRequest("Iron");
    }

    public static ApplySubscriptionRequest InvalidApplySubscriptionRequest()
    {
        return ToApplySubscriptionRequest(null!);
    }

    private static ApplySubscriptionRequest ToApplySubscriptionRequest(string subscription)
    {
        return new ApplySubscriptionRequest
        {
            Subscription = subscription
        };
    }
}