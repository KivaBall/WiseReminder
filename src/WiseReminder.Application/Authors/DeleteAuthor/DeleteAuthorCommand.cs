namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid Id) : ICommand;