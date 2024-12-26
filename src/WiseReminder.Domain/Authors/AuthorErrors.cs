namespace WiseReminder.Domain.Authors;

public static class AuthorErrors
{
    public static IError AuthorNotFound =>
        new Error("Author by Id was not found");

    public static IError InvalidDateDiapason =>
        new Error("Invalid date diapason between birth and death");

    public static IError EitherDeathDateOrUserRelation =>
        new Error("Must be either death date or user relation");
}