using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed class GetQuotesByCategoryIdQueryHandler(IQuoteRepository quoteRepository, IMapper mapper)
    : IQueryHandler<GetQuotesByCategoryIdQuery, ICollection<QuoteVm>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteVm>>> Handle(GetQuotesByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetQuotesByCategoryId(request.CategoryId);

        if (quotes == null)
            return Result<ICollection<QuoteVm>>.Failure<ICollection<QuoteVm>>(null, CategoryErrors.CategoryNotFound);

        return Result<ICollection<QuoteVm>>.Success(_mapper.Map<ICollection<QuoteVm>>(quotes));
    }
}