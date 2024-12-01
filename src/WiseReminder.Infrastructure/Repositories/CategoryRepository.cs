namespace WiseReminder.Infrastructure.Repositories;

public sealed class CategoryRepository(
    AppDbContext context,
    ICategoryService categoryService,
    IQuoteRepository quoteRepository) : ICategoryRepository
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly AppDbContext _context = context;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public void CreateCategory(Category category)
    {
        _context.Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
    }

    public async Task DeleteCategory(Category category)
    {
        _categoryService.DeleteCategory(category);

        var quotes = await _quoteRepository.GetQuotesByCategoryId(category.Id);
        foreach (var quote in quotes!)
        {
            _quoteRepository.DeleteQuote(quote);
        }

        _context.Categories.Update(category);
    }

    public async Task<Category?> GetCategoryById(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<ICollection<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }
}