namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<Category>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        return category != null
            ? Result.Success(category)
            : Result.Failure<Category>(null, CategoryErrors.CategoryNotFound);
    }
}