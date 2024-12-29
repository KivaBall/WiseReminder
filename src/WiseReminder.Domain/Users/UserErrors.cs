namespace WiseReminder.Domain.Users;

public static class UserErrors
{
    public static IError UserNotFound =>
        new Error("User by Id was not found");

    public static IError PasswordNotCorrect =>
        new Error("Password is not correct");

    public static IError UserIdNotValid =>
        new Error("UserId is not valid");
}