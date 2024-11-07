using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand;