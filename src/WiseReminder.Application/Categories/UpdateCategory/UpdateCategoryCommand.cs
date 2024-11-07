using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name, string Description) : ICommand;