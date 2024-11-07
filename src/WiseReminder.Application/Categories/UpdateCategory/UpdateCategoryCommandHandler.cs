using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;

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

        if (category == null) return Result.Failure(CategoryErrors.CategoryNotFound);

        _categoryService.UpdateCategory(category, new CategoryName(request.Name),
            new CategoryDescription(request.Description));
        _categoryRepository.UpdateCategory(category);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}