namespace WiseReminder.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand : ICommand
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}