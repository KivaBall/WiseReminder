namespace WiseReminder.Application.Authors.DeleteAuthorByUser;

public sealed record DeleteAuthorByUserCommand(Guid UserId) : ICommand;