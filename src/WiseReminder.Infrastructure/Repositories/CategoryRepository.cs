namespace WiseReminder.Infrastructure.Repositories;

public sealed class CategoryRepository(
    AppDbContext context)
    : ICategoryRepository
{
    public void CreateCategory(Category category)
    {
        context.Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        context.Categories.Update(category);
    }

    public void DeleteCategory(Category category)
    {
        category.Delete();

        context.Categories.Update(category);
    }

    public async Task<Category?> GetCategoryById(Guid id)
    {
        return await context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<ICollection<Category>> GetAllCategories()
    {
        return await context.Categories
            .ToListAsync();
    }
}