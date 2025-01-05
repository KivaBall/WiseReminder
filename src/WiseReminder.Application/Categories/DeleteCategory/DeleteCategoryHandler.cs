namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed class DeleteCategoryHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryQuery = new GetCategoryByIdQuery(request.Id);

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var quotesQuery = new DeleteQuotesByCategoryIdCommand(request.Id);

        var result = await sender.Send(quotesQuery, cancellationToken);

        if (result.IsFailed)
        {
            return result;
        }

        categoryRepository.DeleteCategory(category.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}