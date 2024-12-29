namespace WiseReminder.Application.Authors.AdminCreateAuthor;

public sealed record AdminCreateAuthorCommand : ICommand<Guid>
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
}