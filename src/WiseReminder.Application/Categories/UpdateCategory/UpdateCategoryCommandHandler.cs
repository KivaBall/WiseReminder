namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery { Id = request.Id };

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var category = result.Value;

        var name = new CategoryName(request.Name);

        var description = new CategoryDescription(request.Description);

        category.Update(name, description);

        categoryRepository.UpdateCategory(category);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok();
    }
}