namespace WiseReminder.Application.Authors.UserCreateAuthor;

public sealed record UserCreateAuthorCommand : ICommand<Guid>
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required Guid UserId { get; init; }
}