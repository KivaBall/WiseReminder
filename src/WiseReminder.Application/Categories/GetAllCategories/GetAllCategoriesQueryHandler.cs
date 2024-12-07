namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetAllCategoriesQuery, ICollection<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<ICollection<CategoryDto>>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategories();

        var dtoCategories = categories.Select(c => c.ToCategoryDto()).ToList();

        return Result.Success<ICollection<CategoryDto>>(dtoCategories);
    }
}