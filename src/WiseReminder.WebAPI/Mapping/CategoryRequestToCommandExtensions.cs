namespace WiseReminder.WebAPI.Mapping;

public static class CategoryRequestToCommandExtensions
{
    public static CreateCategoryCommand ToCreateCategoryCommand(this BaseCategoryRequest request)
    {
        return new CreateCategoryCommand
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static UpdateCategoryCommand ToUpdateCategoryCommand(this UpdateCategoryRequest request)
    {
        return new UpdateCategoryCommand
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };
    }
}