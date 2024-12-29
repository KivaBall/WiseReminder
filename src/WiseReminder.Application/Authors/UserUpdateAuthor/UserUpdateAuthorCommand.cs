namespace WiseReminder.Application.Authors.UserUpdateAuthor;

public sealed record UserUpdateAuthorCommand : ICommand
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required Guid UserId { get; init; }
}