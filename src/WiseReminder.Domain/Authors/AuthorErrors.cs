namespace WiseReminder.Domain.Authors;

public static class AuthorErrors
{
    public static IError AuthorNotFound =>
        new Error("Author by Id was not found");
}