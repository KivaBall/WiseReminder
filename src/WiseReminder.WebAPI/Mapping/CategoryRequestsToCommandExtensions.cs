namespace WiseReminder.WebAPI.Mapping;

public static class CategoryRequestsToCommandExtensions
{
    public static CreateCategoryCommand ToCreateCategoryCommand(
        this CategoryRequest request)
    {
        return new CreateCategoryCommand
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static UpdateCategoryCommand ToUpdateCategoryCommand(
        this CategoryRequest request,
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