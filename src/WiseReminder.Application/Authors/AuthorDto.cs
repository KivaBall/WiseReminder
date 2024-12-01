namespace WiseReminder.Application.Authors;

public sealed record AuthorDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required DateOnly DateOfDeath { get; init; }
}