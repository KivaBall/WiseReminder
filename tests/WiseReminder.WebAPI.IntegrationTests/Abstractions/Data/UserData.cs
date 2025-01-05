namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class UserData
{
    public static string DefaultUsername = "DefaultUsername";
    public static string DefaultLoginForEmptyUser = "DefaultLoginForEmptyUser";
    public static string DefaultLoginForUserWithData = "DefaultLoginForUserWithData";
    public static string DefaultPassword = "DefaultPassword";

    public static string UpdatedUsername = "UpdatedUsername";
    public static string UpdatedPassword = "UpdatedPassword";

    public static UserRequest CreateEmptyUserRequest =>
        UserRequest(DefaultUsername, DefaultLoginForEmptyUser, DefaultPassword);

    public static UserRequest CreateUserWithDataRequest =>
        UserRequest(DefaultUsername, DefaultLoginForUserWithData, DefaultPassword);

    public static UserRequest InvalidUserRequest =>
        UserRequest(null!, null!, null!);

    public static ChangeUsernameRequest DefaultChangeUsernameRequest =>
        ChangeUsernameRequest(UpdatedUsername, DefaultPassword);

    public static ChangeUsernameRequest InvalidChangeUsernameRequest =>
        ChangeUsernameRequest(null!, null!);

    public static ChangePasswordRequest DefaultChangePasswordRequest =>
        ChangePasswordRequest(DefaultPassword, UpdatedPassword);

    public static ChangePasswordRequest InvalidChangePasswordRequest =>
        ChangePasswordRequest(null!, null!);

    public static UserLoginRequest EmptyUserLoginRequest =>
        UserLoginRequest(DefaultLoginForEmptyUser, DefaultPassword);

    public static UserLoginRequest UserWithDataLoginRequest =>
        UserLoginRequest(DefaultLoginForUserWithData, DefaultPassword);

    public static UserLoginRequest InvalidUserLoginRequest =>
        UserLoginRequest(null!, null!);

    public static AdminLoginRequest DefaultAdminLoginRequest =>
        AdminLoginRequest("first_secret_admin_password", "second_secret_admin_password",
            "third_secret_admin_password");

    public static AdminLoginRequest InvalidAdminLoginRequest =>
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

    private static UserLoginRequest UserLoginRequest(string login, string password)
    {
        return new UserLoginRequest
        {
            Login = login,
            Password = password
        };
    }
}