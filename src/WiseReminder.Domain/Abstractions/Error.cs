namespace WiseReminder.Domain.Abstractions;

public sealed record Error(string Title, string Message)
{
    public static Error None => new(string.Empty, string.Empty);
    public static Error Unknown => new("Unknown error", "Received an unknown error in program");
    public static Error Database => new("Database error", "Something went wrong with database");
}