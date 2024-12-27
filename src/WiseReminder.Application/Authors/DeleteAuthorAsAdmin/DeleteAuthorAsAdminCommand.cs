namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed record DeleteAuthorAsAdminCommand : ICommand
{
    public required Guid Id { get; init; }
}