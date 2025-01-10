namespace WiseReminder.Application.Categories.Queries.GetCategoryDtoById;

public sealed class GetCategoryDtoByIdHandler(
    ISender sender)
    : IQueryHandler<GetCategoryDtoByIdQuery, CategoryDto>
{
    public async Task<Result<CategoryDto>> Handle(
        GetCategoryDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryDetailsByIdQuery(request.Id);

        var category = await sender.Send(query, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var categoryDto = category.Value.ToCategoryDto();

        return Result.Ok(categoryDto);
    }
}