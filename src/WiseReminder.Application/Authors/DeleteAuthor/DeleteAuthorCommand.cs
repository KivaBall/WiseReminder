namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed record DeleteAuthorCommand : ICommand
{
    public Guid Id { get; init; }
}