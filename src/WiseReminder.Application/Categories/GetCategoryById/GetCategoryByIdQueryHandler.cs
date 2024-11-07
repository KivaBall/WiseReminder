using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Categories.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IQueryHandler<GetCategoryByIdQuery, CategoryVm>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CategoryVm>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);

        if (category == null) return Result<CategoryVm>.Failure<CategoryVm>(null, CategoryErrors.CategoryNotFound);

        return Result<CategoryVm>.Success(_mapper.Map<CategoryVm>(category));
    }
}