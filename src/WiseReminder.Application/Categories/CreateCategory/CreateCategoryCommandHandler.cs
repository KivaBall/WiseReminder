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

        var description = new Description(request.Description);

        var category = new Category(name, description);

        categoryRepository.CreateCategory(category);

        return await unitOfWork.SaveChangesAsyncWithResult(() => category.Id);
    }
}