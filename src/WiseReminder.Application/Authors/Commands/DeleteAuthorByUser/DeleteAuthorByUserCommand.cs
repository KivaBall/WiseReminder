namespace WiseReminder.Application.Authors.Commands.DeleteAuthorByUser;

public sealed record DeleteAuthorByUserCommand(Guid UserId) : ICommand;