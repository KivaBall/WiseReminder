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
            return category.ToResult();
        }

        categoryRepository.DeleteCategory(category.Value);

        var quotesQuery = new GetQuotesByCategoryIdQuery { CategoryId = request.Id };

        var quotes = await sender.Send(quotesQuery, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        return await unitOfWork.SaveChangesAsync();
    }
}