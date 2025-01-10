namespace WiseReminder.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : ICommand;