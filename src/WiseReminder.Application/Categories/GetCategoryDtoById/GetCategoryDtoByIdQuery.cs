namespace WiseReminder.Application.Categories.GetCategoryDtoById;

public sealed record GetCategoryDtoByIdQuery : IQuery<CategoryDto>
{
    public Guid Id { get; init; }
}