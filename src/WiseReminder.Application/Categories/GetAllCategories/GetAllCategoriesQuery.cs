using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed record GetAllCategoriesQuery : IQuery<ICollection<CategoryVm>>;