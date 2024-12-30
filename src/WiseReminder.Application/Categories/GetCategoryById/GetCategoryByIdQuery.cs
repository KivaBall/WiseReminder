namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<Category>;