namespace WiseReminder.Application.Categories.CreateCategory;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var name = new CategoryName(request.Name);
        var description = new CategoryDescription(request.Description);

        var category = new Category(name, description);

        categoryRepository.CreateCategory(category);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok(category.Id);
    }
}