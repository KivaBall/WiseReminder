namespace WiseReminder.Application.Categories.Queries.GetCategoryDetailsById;

public sealed class GetCategoryDetailsByIdHandler(
    ICategoryRepository repository)
    : IQueryHandler<GetCategoryDetailsByIdQuery, CategoryDetails>
{
    public async Task<Result<CategoryDetails>> Handle(
        GetCategoryDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        var categoryDetails =
            await repository.GetCategoryDetailsById(request.Id, cancellationToken);

        if (categoryDetails == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        return Result.Ok(categoryDetails);
    }
}