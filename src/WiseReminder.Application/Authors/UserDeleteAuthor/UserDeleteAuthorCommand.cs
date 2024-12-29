namespace WiseReminder.Application.Authors.UserDeleteAuthor;

public sealed record UserDeleteAuthorCommand(Guid UserId) : ICommand;