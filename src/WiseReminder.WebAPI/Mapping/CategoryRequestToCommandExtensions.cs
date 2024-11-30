namespace WiseReminder.WebAPI.Mapping;

public static class CategoryRequestToCommandExtensions
{
    public static CreateCategoryCommand ToCreateCategoryCommand(this BaseCategoryRequest request)
    {
        if (request.Name == null
            || request.Description == null)
        {
            throw new ArgumentNullException($"{nameof(BaseCategoryRequest)} has null property");
        }

        return new CreateCategoryCommand
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static UpdateCategoryCommand ToUpdateCategoryCommand(this UpdateCategoryRequest request)
    {
        if (request.Id == null
            || request.Name == null
            || request.Description == null)
        {
            throw new ArgumentNullException($"{nameof(UpdateCategoryRequest)} has null property");
        }

        return new UpdateCategoryCommand
        {
            Id = (Guid)request.Id,
            Name = request.Name,
            Description = request.Description
        };
    }
}