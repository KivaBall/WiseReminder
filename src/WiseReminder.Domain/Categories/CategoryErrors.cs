namespace WiseReminder.Domain.Categories;

public static class CategoryErrors
{
    public static Result CategoryNotFound =>
        new Error("The category with the specified ID was not found");
}