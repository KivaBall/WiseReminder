namespace WiseReminder.Application.Abstractions.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this CategoryDetails categoryDetails)
    {
        return new CategoryDto
        {
            Id = categoryDetails.Category.Id,
            Name = categoryDetails.Category.Name.Value,
            Description = categoryDetails.Category.Description.Value,
            Quotes = categoryDetails.Quotes
        };
    }
}