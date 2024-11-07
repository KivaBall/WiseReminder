namespace WiseReminder.Domain.Categories;

public interface ICategoryService
{
    Category CreateCategory(CategoryName name, CategoryDescription description);
    Category UpdateCategory(Category category, CategoryName name, CategoryDescription description);
    Category DeleteCategory(Category category);
}