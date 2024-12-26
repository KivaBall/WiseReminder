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

        var category = await sender.Send(query);

        return category.IsFailed
            ? Result.Fail(category.Errors)
            : Result.Ok(category.Value.ToCategoryDto());
    }
}