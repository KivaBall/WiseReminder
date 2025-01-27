namespace WiseReminder.Domain.Users;

public static class UserErrors
{
    public static readonly Result UserNotFound =
        new Error("The user with the specified ID was not found");

    public static readonly Result PasswordNotCorrect =
        new Error("The provided password is incorrect");

    public static readonly Result LoginAlreadyExists =
        new Error("The provided login already exists");

    public static readonly Result IncorrectNameOfSubscription =
        new Error("The name of subscription is incorrect");

    public static readonly Result NothingUpdated =
        new Error("Nothing was updated. You must write either username or password");
}