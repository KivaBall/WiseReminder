namespace WiseReminder.WebAPI.Controllers.Categories;

public record BaseCategoryRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}