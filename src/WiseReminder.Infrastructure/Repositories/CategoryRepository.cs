namespace WiseReminder.Infrastructure.Repositories;

public sealed class CategoryRepository(
    AppDbContext context,
    IMemoryCache memoryCache)
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
        var key = $"category-{id}";

        return await memoryCache.GetOrCreateAsync(key, async factory =>
        {
            factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return await context.Categories.FirstOrDefaultAsync(category => category.Id == id);
        });
    }

    public async Task<ICollection<Category>> GetAllCategories()
    {
        var key = "category-all";

        return await memoryCache.GetOrCreateAsync(key, async factory =>
        {
            factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return await context.Categories.ToListAsync();
        }) ?? [];
    }
}