using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed class GetQuotesByAuthorIdQueryHandler(IQuoteRepository quoteRepository, IMapper mapper)
    : IQueryHandler<GetQuotesByAuthorIdQuery, ICollection<QuoteVm>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<ICollection<QuoteVm>>> Handle(GetQuotesByAuthorIdQuery request,
        CancellationToken cancellationToken)
    {
        var quotes = await _quoteRepository.GetQuotesByAuthorId(request.AuthorId);

        if (quotes == null)
            return Result<ICollection<QuoteVm>>.Failure<ICollection<QuoteVm>>(null, AuthorErrors.AuthorNotFound);

        return Result<ICollection<QuoteVm>>.Success(_mapper.Map<ICollection<QuoteVm>>(quotes));
    }
}