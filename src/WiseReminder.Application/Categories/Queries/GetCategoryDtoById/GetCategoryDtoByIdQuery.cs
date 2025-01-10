namespace WiseReminder.Application.Categories.Queries.GetCategoryDtoById;

public sealed record GetCategoryDtoByIdQuery(Guid Id) : IQuery<CategoryDto>;