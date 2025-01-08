namespace WiseReminder.Application.Abstractions.Mappers;

public static class CategoryMapper
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