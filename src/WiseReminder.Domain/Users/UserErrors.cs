namespace WiseReminder.Domain.Users;

public static class UserErrors
{
    public static Result UserNotFound =>
        new Error("The user with the specified ID was not found");

    public static Result PasswordNotCorrect =>
        new Error("The provided password is incorrect");

    public static Result UserIdNotValid =>
        new Error("The specified User ID is invalid");

    public static Result LoginAlreadyExists =>
        new Error("The provided login already exists");

    public static Result IncorrectNameOfSubscription =>
        new Error("The name of subscription is incorrect");

    public static Result NothingUpdated =>
        new Error("Nothing was updated. You must write either username or password");
}