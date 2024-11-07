using AutoMapper;
using WiseReminder.Application.Authors;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Abstractions.AutoMapper;

public sealed class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorVm>()
            .ForCtorParam("Id", options => options.MapFrom(author => author.Id))
            .ForCtorParam("Name", options => options.MapFrom(author => author.Name.Value))
            .ForCtorParam("Biography", options => options.MapFrom(author => author.Biography.Value))
            .ForCtorParam("DateOfBirth", options => options.MapFrom(author => author.DateOfBirth.Value))
            .ForCtorParam("DateOfDeath", options => options.MapFrom(author => author.DateOfDeath.Value));
    }
}