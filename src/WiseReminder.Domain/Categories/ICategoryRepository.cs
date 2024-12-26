namespace WiseReminder.Domain.Categories;

public interface ICategoryRepository
{
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    Task DeleteCategory(Category category); //TODO: deletion must not be Task!
    Task<Category?> GetCategoryById(Guid id);
    Task<ICollection<Category>> GetAllCategories();
}