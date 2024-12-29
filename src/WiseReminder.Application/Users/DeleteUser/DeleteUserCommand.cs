namespace WiseReminder.Application.Users.DeleteUser;

public sealed record DeleteUserCommand : ICommand
{
    public required Guid Id { get; init; }
}