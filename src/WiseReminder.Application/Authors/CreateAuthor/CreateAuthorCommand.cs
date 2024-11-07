using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand(string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath)
    : ICommand;