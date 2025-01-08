namespace WiseReminder.Domain.Categories;

public interface ICategoryRepository
{
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
    Task<Category?> GetCategoryById(Guid id, CancellationToken cancellationToken);
    Task<bool> HasCategoryById(Guid id, CancellationToken cancellationToken);
    Task<ICollection<Category>> GetAllCategories(CancellationToken cancellationToken);
}