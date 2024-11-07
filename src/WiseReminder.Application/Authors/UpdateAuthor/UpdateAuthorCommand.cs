using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    Guid Id,
    string Name,
    string Biography,
    DateOnly DateOfBirth,
    DateOnly DateOfDeath) : ICommand;