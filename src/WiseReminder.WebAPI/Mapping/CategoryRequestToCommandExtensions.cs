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

    public static UpdateCategoryCommand ToUpdateCategoryCommand(this BaseCategoryRequest request,
        Guid id)
    {
        return new UpdateCategoryCommand
        {
            Id = id,
            Name = request.Name,
            Description = request.Description
        };
    }
}