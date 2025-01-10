namespace WiseReminder.Application.Categories.Commands.DeleteCategory;

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

        repository.DeleteCategory(category.Value);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result.IsSuccess)
        {
            var categoryDeletedEvent = new CategoryDeletedEvent(request.Id);

            await mediator.Publish(categoryDeletedEvent, cancellationToken);
        }

        return result;
    }
}