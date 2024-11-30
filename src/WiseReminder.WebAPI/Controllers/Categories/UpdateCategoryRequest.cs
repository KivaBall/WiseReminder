namespace WiseReminder.WebAPI.Controllers.Categories;

public sealed record UpdateCategoryRequest : BaseCategoryRequest
{
    public Guid? Id { get; init; }
}