namespace WiseReminder.Domain.UnitTests.Data;

public static class CategoryData
{
    public static CategoryName CategoryName => new CategoryName("CategoryName");

    public static CategoryName NewCategoryName => new CategoryName("NewCategoryName");

    public static Description Description => new Description("Description");

    public static Description NewDescription => new Description("NewDescription");

    public static Category Category => new Category(CategoryName, Description);
}