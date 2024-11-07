using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand(string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath)
    : ICommand;