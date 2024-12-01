namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand : ICommand
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required DateOnly DateOfDeath { get; init; }
}