namespace WiseReminder.Domain.Authors;

public static class AuthorErrors
{
    public static Result AuthorNotFound =>
        new Error("Author with the specified ID was not found");

    public static Result AuthorQuoteDatesNotFound =>
        new Error("Quote dates of author with the specified ID was not found");

    public static Result UserAuthorNotFound =>
        new Error("The author has not been created for the user yet");

    public static Result InvalidBirthAndDeathDateRange =>
        new Error("The specified date range between birth and death is invalid");

    public static Result InvalidBirthAndMinQuoteDateRange =>
        new Error("The specified date range between birth and minimal quote date is invalid");

    public static Result InvalidDeathAndMaxQuoteDateRange =>
        new Error("The specified date range between death and maximal quote date is invalid");

    public static Result AccessToModifyAuthorDenied =>
        new Error("You are not allowed to modify author data by access");

    public static Result DuplicateAuthorForUser =>
        new Error("An author already exists for the user");
}