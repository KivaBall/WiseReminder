namespace WiseReminder.Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryHandler(
    ICategoryRepository repository,
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

        repository.CreateCategory(category);

        return await unitOfWork.SaveChangesAsync(category.Id, cancellationToken);
    }
}