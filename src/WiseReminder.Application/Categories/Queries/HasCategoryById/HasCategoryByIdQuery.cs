namespace WiseReminder.Application.Categories.Queries.HasCategoryById;

public sealed record HasCategoryByIdQuery(Guid Id) : IQuery<bool>;