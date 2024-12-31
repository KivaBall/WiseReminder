namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class UserData
{
    public static string DefaultUsername = "DefaultUsername";
    public static string DefaultLogin = "DefaultLogin";
    public static string DefaultPassword = "DefaultPassword";

    public static string UpdatedUsername = "UpdatedUsername";
    public static string UpdatedPassword = "UpdatedPassword";

    public static UserRequest CreateUserRequest =>
        UserRequest(DefaultUsername, DefaultLogin, DefaultPassword);

    public static UserRequest NotValidUserRequest =>
        UserRequest(null!, null!, null!);

    public static ChangeUsernameRequest DefaultChangeUsernameRequest =>
        ChangeUsernameRequest(UpdatedUsername, DefaultPassword);

    public static ChangeUsernameRequest NotValidChangeUsernameRequest =>
        ChangeUsernameRequest(null!, null!);

    public static ChangePasswordRequest DefaultChangePasswordRequest =>
        ChangePasswordRequest(DefaultPassword, UpdatedPassword);

    public static ChangePasswordRequest NotValidChangePasswordRequest =>
        ChangePasswordRequest(null!, null!);

    public static UserLoginRequest DefaultUserLoginRequest =>
        UserLoginRequest(DefaultLogin, DefaultPassword);

    public static UserLoginRequest UpdatedUserLoginRequest =>
        UserLoginRequest(DefaultLogin, UpdatedPassword);

    public static UserLoginRequest NotValidUserLoginRequest =>
        UserLoginRequest(null!, null!);

    public static AdminLoginRequest DefaultAdminLoginRequest =>
        AdminLoginRequest("first_secret_admin_password", "second_secret_admin_password",
            "third_secret_admin_password");

    public static AdminLoginRequest NotValidAdminLoginRequest =>
        AdminLoginRequest(null!, null!, null!);

    private static UserRequest UserRequest(string username, string login, string password)
    {
        return new UserRequest
        {
            Username = username,
            Login = login,
            Password = password
        };
    }

    private static ChangeUsernameRequest ChangeUsernameRequest(string newUsername, string password)
    {
        return new ChangeUsernameRequest
        {
            NewUsername = newUsername,
            Password = password
        };
    }

    private static ChangePasswordRequest ChangePasswordRequest(string oldPassword,
        string newPassword)
    {
        return new ChangePasswordRequest
        {
            OldPassword = oldPassword,
            NewPassword = newPassword
        };
    }

    private static UserLoginRequest UserLoginRequest(string login, string password)
    {
        return new UserLoginRequest
        {
            Login = login,
            Password = password
        };
    }

    private static AdminLoginRequest AdminLoginRequest(string firstPassword, string secondPassword,
        string thirdPassword)
    {
        return new AdminLoginRequest
        {
            FirstPassword = firstPassword,
            SecondPassword = secondPassword,
            ThirdPassword = thirdPassword
        };
    }
}