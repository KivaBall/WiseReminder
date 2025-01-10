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

    public async Task<Category?> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<CategoryDetails?> GetCategoryDetailsById(Guid id,
        CancellationToken cancellationToken)
    {
        return await context.Categories
            .Where(category => category.Id == id)
            .ConvertToCategoryDetails(context)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> HasCategoryById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Categories
            .AnyAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<ICollection<CategoryDetails>> GetAllCategories(
        CancellationToken cancellationToken)
    {
        return await context.Categories
            .ConvertToCategoryDetails(context)
            .ToListAsync(cancellationToken);
    }
}