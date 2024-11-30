using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
}