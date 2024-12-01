namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}