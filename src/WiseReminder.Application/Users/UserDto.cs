namespace WiseReminder.Application.Users;

public sealed record UserDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Login { get; init; }
    public required Subscription Subscription { get; init; }
    public required Guid? AuthorId { get; init; }
}