﻿namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdHandler(
    ICategoryRepository repository)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Result<Category>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await repository.GetCategoryById(request.Id);

        if (category == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        return Result.Ok(category);
    }
}