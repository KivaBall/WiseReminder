namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Result<Category>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryById(request.Id);

        if (category == null)
        {
            return Result.Fail(CategoryErrors.CategoryNotFound);
        }

        return Result.Ok(category);
    }
}