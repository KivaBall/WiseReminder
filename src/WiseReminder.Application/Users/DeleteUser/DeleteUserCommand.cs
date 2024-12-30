namespace WiseReminder.Application.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : ICommand;