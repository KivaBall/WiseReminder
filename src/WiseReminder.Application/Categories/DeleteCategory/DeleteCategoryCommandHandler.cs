using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Categories.DeleteCategory;

public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        if (category == null) return Result.Failure(CategoryErrors.CategoryNotFound);

        _categoryRepository.DeleteCategory(category);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}