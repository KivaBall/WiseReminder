namespace WiseReminder.Domain.Categories;

public static class CategoryErrors
{
    public static IError CategoryNotFound =>
        new Error("Category by Id was not found");
}