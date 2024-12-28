namespace WiseReminder.Application.Users.LoginAsUser;

public sealed record LoginAsUserCommand : ICommand<string>
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}