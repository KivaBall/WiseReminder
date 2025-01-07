namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed class DeleteCategoryHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    IMediator mediator)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryQuery = new GetCategoryByIdQuery(request.Id);

        var category = await mediator.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var categoryDeletedEvent = new CategoryDeletedEvent(request.Id);

        await mediator.Publish(categoryDeletedEvent, cancellationToken);

        repository.DeleteCategory(category.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}