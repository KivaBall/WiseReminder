namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetAllCategoriesQuery, ICollection<CategoryDto>>
{
    public async Task<Result<ICollection<CategoryDto>>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllCategories();

        var dtoCategories = categories
            .Select(c => c.ToCategoryDto())
            .ToList();

        return Result.Ok<ICollection<CategoryDto>>(dtoCategories);
    }
}