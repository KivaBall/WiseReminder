namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed record GetCategoryByIdQuery : IQuery<Category>
{
    public Guid Id { get; init; }
}