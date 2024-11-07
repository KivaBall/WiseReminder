using WiseReminder.Domain.Abstractions;

namespace WiseReminder.Domain.Categories;

public static class CategoryErrors
{
    public static Error CategoryNotFound => new("Category error", "Category by Id was not found");
}