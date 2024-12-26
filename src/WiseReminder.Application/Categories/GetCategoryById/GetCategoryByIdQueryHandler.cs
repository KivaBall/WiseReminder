namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Result<Category>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryById(request.Id);

        return category == null
            ? Result.Fail(CategoryErrors.CategoryNotFound)
            : Result.Ok(category);
    }
}