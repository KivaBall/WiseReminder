using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand : ICommand
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}