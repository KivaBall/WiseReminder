namespace WiseReminder.Domain.Categories;

public class CategoryService : ICategoryService
{
    public Category CreateCategory(CategoryName name, CategoryDescription description)
    {
        return new Category(name, description);
    }

    public Category UpdateCategory(Category category, CategoryName name, CategoryDescription description)
    {
        category.Name = name;
        category.Description = description;
        return category;
    }

    public Category DeleteCategory(Category category)
    {
        return (Category)category.DeleteEntity();
    }
}