using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand : ICommand
{
    public string Name { get; init; }
    public string Biography { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public DateOnly DateOfDeath { get; init; }
}