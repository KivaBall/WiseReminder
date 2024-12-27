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

        var category = await sender.Send(query, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var name = new CategoryName(request.Name);

        var description = new Description(request.Description);

        category.Value.Update(name, description);

        categoryRepository.UpdateCategory(category.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}