namespace WiseReminder.Application.Categories.GetCategoryDtos;

public sealed class GetCategoryDtosHandler(
    ICategoryRepository repository)
    : IQueryHandler<GetCategoryDtosQuery, ICollection<CategoryDto>>
{
    public async Task<Result<ICollection<CategoryDto>>> Handle(
        GetCategoryDtosQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllCategories();

        ICollection<CategoryDto> categoryDtos = categories
            .Select(c => c.ToCategoryDto())
            .ToList();

        return Result.Ok(categoryDtos);
    }
}