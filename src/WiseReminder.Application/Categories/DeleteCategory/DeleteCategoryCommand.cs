namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed record DeleteCategoryCommand : ICommand
{
    public required Guid Id { get; init; }
}