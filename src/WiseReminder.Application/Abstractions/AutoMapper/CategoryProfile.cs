using AutoMapper;
using WiseReminder.Application.Categories;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Abstractions.AutoMapper;

public sealed class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryVm>()
            .ForCtorParam("Id", options => options.MapFrom(category => category.Id))
            .ForCtorParam("Name", options => options.MapFrom(category => category.Name.Value))
            .ForCtorParam("Description", options => options.MapFrom(category => category.Description.Value));
    }
}