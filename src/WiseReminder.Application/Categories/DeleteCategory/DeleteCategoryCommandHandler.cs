namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryQuery = new GetCategoryByIdQuery { Id = request.Id };

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return Result.Fail(category.Errors);
        }

        categoryRepository.DeleteCategory(category.Value);

        var quotesQuery = new GetQuotesByCategoryIdQuery { CategoryId = request.Id };

        var quotesResult = await sender.Send(quotesQuery, cancellationToken);

        if (quotesResult.IsFailed)
        {
            return Result.Fail(quotesResult.Errors);
        }

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}