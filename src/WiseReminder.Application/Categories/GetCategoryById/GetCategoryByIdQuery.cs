using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryVm>;