namespace WiseReminder.Domain.Categories;

public interface ICategoryRepository
{
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
    Task<Category?> GetCategoryById(Guid id);
    Task<ICollection<Category>> GetAllCategories();
}