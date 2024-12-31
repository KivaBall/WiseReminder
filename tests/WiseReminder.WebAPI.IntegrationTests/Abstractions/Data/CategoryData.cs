namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class CategoryData
{
    public static string DefaultName = "DefaultName";
    public static string DefaultDescription = "DefaultDescription";

    public static string UpdatedName = "UpdatedName";
    public static string UpdatedDescription = "UpdatedDescription";

    public static CategoryRequest CreateCategoryRequest =>
        CategoryRequest(DefaultName, DefaultDescription);

    public static CategoryRequest UpdateCategoryRequest =>
        CategoryRequest(UpdatedName, UpdatedDescription);

    public static CategoryRequest NotValidCategoryRequest =>
        CategoryRequest(null!, null!);

    private static CategoryRequest CategoryRequest(string name, string description)
    {
        return new CategoryRequest
        {
            Name = name,
            Description = description
        };
    }
}