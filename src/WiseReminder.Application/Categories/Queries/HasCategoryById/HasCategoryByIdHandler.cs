namespace WiseReminder.Application.Categories.Queries.HasCategoryById;

public sealed class HasCategoryByIdHandler(
    ICategoryRepository repository)
    : IQueryHandler<HasCategoryByIdQuery, bool>
{
    public async Task<Result<bool>> Handle(
        HasCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var categoryExists = await repository.HasCategoryById(request.Id, cancellationToken);

        if (!categoryExists)
        {
            return CategoryErrors.CategoryNotFound;
        }

        return Result.Ok(true);
    }
}