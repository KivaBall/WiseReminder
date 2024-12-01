namespace WiseReminder.Application.Categories.CreateCategory;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    ICategoryService categoryService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryName = new CategoryName(request.Name);
        var categoryDescription = new CategoryDescription(request.Description);

        var category = _categoryService.CreateCategory(categoryName, categoryDescription);

        _categoryRepository.CreateCategory(category);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}