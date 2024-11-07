using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Categories.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IQueryHandler<GetAllCategoriesQuery, ICollection<CategoryVm>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ICollection<CategoryVm>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategories();

        return Result<ICollection<CategoryVm>>.Success(_mapper.Map<ICollection<CategoryVm>>(categories));
    }
}