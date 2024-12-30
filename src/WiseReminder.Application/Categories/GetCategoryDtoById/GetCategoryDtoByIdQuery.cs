namespace WiseReminder.Application.Categories.GetCategoryDtoById;

public sealed record GetCategoryDtoByIdQuery(Guid Id) : IQuery<CategoryDto>;