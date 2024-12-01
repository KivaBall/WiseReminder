namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        if (category == null)
        {
            return Result.Failure<CategoryDto>(null, CategoryErrors.CategoryNotFound);
        }

        return Result.Success(category.ToCategoryDto());
    }
}