namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class CategoryData
{
    public const string DefaultName = "DefaultName";
    public const string DefaultDescription = "DefaultDescription";

    public const string UpdatedName = "UpdatedName";
    public const string UpdatedDescription = "UpdatedDescription";

    public static BaseCategoryRequest BaseCategoryRequest()
    {
        return new BaseCategoryRequest
        {
            Name = DefaultName,
            Description = DefaultDescription
        };
    }

    public static BaseCategoryRequest NotValidBaseCategoryRequest()
    {
        return new BaseCategoryRequest
        {
            Name = null!,
            Description = null!
        };
    }

    public static UpdateCategoryRequest UpdateCategoryRequest(Guid id)
    {
        return new UpdateCategoryRequest
        {
            Id = id,
            Name = UpdatedName,
            Description = UpdatedDescription
        };
    }

    public static UpdateCategoryRequest NotValidUpdateCategoryRequest(Guid id)
    {
        return new UpdateCategoryRequest
        {
            Id = id,
            Name = null!,
            Description = null!
        };
    }
}