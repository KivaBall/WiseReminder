namespace WiseReminder.Application.Categories.GetCategoryDtoById;

public sealed record GetCategoryDtoByIdQuery : IQuery<CategoryDto>
{
    public required Guid Id { get; init; }
}