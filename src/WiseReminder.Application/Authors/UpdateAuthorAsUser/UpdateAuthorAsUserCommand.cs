namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorAsUserCommand : ICommand
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
    public required Guid UserId { get; init; }
}