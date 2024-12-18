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

        var result = await sender.Send(query);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        return Result.Ok(result.Value.ToCategoryDto());
    }
}