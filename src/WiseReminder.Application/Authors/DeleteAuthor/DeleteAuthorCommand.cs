namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed record DeleteAuthorCommand : ICommand
{
    public required Guid Id { get; init; }
}