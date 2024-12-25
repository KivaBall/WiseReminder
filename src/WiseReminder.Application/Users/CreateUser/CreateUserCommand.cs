namespace WiseReminder.Application.Users.CreateUser;

public sealed record CreateUserCommand : ICommand<Guid>
{
    public string Username { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
}