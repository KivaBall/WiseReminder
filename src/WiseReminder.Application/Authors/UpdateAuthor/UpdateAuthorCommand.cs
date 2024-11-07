using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Categories.UpdateCategory;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorCommand(Guid Id, string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath) : ICommand;