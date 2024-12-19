namespace WiseReminder.WebAPI.Controllers.Categories;

public sealed record UpdateCategoryRequest : BaseCategoryRequest
{
    public required Guid Id { get; init; }
}