namespace WiseReminder.Application.Abstractions.Mapping;

public static class CategoryToDtoMapper
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name.Value,
            Description = category.Description.Value
        };
    }
}