using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string Description) : ICommand;