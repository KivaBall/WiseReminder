namespace WiseReminder.Application.Authors.AdminUpdateAuthor;

public sealed record AdminUpdateAuthorCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
}