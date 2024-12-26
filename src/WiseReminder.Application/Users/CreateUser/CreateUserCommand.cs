namespace WiseReminder.Application.Users.CreateUser;

public sealed record CreateUserCommand : ICommand<Guid>
{
    public required string Username { get; init; }
    public required string Login { get; init; }
    public required string Password { get; init; }
}