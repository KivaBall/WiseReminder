namespace WiseReminder.Application.Authors.Commands.DeleteAuthorByAdmin;

public sealed record DeleteAuthorByAdminCommand(Guid Id) : ICommand;