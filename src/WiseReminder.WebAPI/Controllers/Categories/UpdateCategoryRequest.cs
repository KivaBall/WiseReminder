namespace WiseReminder.WebAPI.Controllers.Categories;

public sealed record UpdateCategoryRequest(Guid Id, string Name, string Description);