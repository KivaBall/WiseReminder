namespace WiseReminder.Application.Users.ChangeUsername;

public sealed record ChangeUsernameCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string NewUsername { get; init; }
    public required string Password { get; init; }
}