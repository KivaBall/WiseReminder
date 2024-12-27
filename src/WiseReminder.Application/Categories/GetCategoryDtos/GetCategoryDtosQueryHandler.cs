namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed class GetCategoryDtosQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryDtosQuery, ICollection<CategoryDto>>
{
    public async Task<Result<ICollection<CategoryDto>>> Handle(
        GetCategoryDtosQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllCategories();

        var dtoCategories = categories
            .Select(c => c.ToCategoryDto())
            .ToList();

        return Result.Ok<ICollection<CategoryDto>>(dtoCategories);
    }
}