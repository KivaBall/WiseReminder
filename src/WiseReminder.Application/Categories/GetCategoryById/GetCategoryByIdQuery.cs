namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed record GetCategoryByIdQuery : IQuery<Category>
{
    public required Guid Id { get; init; }
}