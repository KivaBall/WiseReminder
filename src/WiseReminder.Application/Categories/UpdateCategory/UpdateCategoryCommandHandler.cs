namespace WiseReminder.Application.Categories.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    ICategoryService categoryService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        if (category == null)
        {
            return Result.Failure(CategoryErrors.CategoryNotFound);
        }

        var categoryName = new CategoryName(request.Name);
        var categoryDescription = new CategoryDescription(request.Description);

        _categoryService.UpdateCategory(category, categoryName, categoryDescription);
        _categoryRepository.UpdateCategory(category);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}