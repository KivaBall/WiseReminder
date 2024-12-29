namespace WiseReminder.Application.Authors.DeleteAuthorAsAdmin;

public sealed record DeleteAuthorAsAdminCommand : ICommand
{
    public required Guid Id { get; init; }
}