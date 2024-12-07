namespace WiseReminder.Application.Categories.GetCategoryDtoById;

public sealed class GetCategoryDtoByIdQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryDtoByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<CategoryDto>> Handle(
        GetCategoryDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        return category != null
            ? Result.Success(category.ToCategoryDto())
            : Result.Failure<CategoryDto>(null, CategoryErrors.CategoryNotFound);
    }
}