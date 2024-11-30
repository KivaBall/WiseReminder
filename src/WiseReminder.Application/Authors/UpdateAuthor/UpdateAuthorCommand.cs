using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorCommand : ICommand
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Biography { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public DateOnly DateOfDeath { get; init; }
}