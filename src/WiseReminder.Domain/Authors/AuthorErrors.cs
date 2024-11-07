using WiseReminder.Domain.Abstractions;

namespace WiseReminder.Domain.Authors;

public static class AuthorErrors
{
    public static Error AuthorNotFound => new("Author error", "Author by Id was not found");
}