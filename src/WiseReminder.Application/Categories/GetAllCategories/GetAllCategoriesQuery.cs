namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed record GetAllCategoriesQuery : IQuery<ICollection<CategoryDto>>;