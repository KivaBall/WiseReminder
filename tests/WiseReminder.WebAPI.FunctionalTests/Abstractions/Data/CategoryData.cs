namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class CategoryData
{
    public const string Name = "Name";
    public const string NewName = "NewName";

    public const string Description = "Description";
    public const string NewDescription = "NewDescription";

    public static CategoryRequest CreateCategoryRequest()
    {
        return ToCategoryRequest(Name, Description);
    }

    public static CategoryRequest UpdateCategoryRequest()
    {
        return ToCategoryRequest(NewName, NewDescription);
    }

    public static CategoryRequest InvalidCategoryRequest()
    {
        return ToCategoryRequest(null!, null!);
    }

    private static CategoryRequest ToCategoryRequest(string name, string description)
    {
        return new CategoryRequest
        {
            Name = name,
            Description = description
        };
    }
}