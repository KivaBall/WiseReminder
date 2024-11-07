using AutoMapper;
using WiseReminder.Application.Quotes;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Abstractions.AutoMapper;

public sealed class QuoteProfile : Profile
{
    public QuoteProfile()
    {
        CreateMap<Quote, QuoteVm>()
            .ForCtorParam("Id", options => options.MapFrom(author => author.Id))
            .ForCtorParam("Text", options => options.MapFrom(author => author.Text.Value))
            .ForCtorParam("AuthorId", options => options.MapFrom(author => author.AuthorId))
            .ForCtorParam("CategoryId", options => options.MapFrom(author => author.CategoryId))
            .ForCtorParam("QuoteDate", options => options.MapFrom(author => author.QuoteDate.Value));
    }
}