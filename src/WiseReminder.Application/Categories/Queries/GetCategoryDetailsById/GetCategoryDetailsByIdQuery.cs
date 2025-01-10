namespace WiseReminder.Application.Categories.Queries.GetCategoryDetailsById;

public sealed record GetCategoryDetailsByIdQuery(Guid Id) : IQuery<CategoryDetails>;