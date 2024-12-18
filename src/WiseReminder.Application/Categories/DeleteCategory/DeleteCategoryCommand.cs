namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed record DeleteCategoryCommand : ICommand
{
    public Guid Id { get; init; }
}