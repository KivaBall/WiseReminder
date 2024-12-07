namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCategoryByIdQuery(request.Id), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error);
        }

        var category = result.Entity!;

        await _categoryRepository.DeleteCategory(category);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}