namespace WiseReminder.Application.Categories.GetCategoryDtoById;

public sealed class GetCategoryDtoByIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetCategoryDtoByIdQuery, CategoryDto>
{
    public async Task<Result<CategoryDto>> Handle(
        GetCategoryDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery { Id = request.Id };

        var category = await sender.Send(query, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var categoryDto = category.Value.ToCategoryDto();

        return Result.Ok(categoryDto);
    }
}