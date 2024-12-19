namespace WiseReminder.WebAPI.Controllers.Categories;

public record BaseCategoryRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}