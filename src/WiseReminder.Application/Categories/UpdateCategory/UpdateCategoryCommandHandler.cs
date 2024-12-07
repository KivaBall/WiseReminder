namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    ICategoryService categoryService,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCategoryByIdQuery(request.Id), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error);
        }

        var category = result.Entity!;

        var categoryName = new CategoryName(request.Name);
        var categoryDescription = new CategoryDescription(request.Description);

        _categoryService.UpdateCategory(
            category,
            categoryName,
            categoryDescription);

        _categoryRepository.UpdateCategory(category);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}