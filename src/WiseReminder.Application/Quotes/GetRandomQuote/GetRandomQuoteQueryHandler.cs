using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.GetRandomQuote;

public sealed class GetRandomQuoteQueryHandler(IQuoteRepository quoteRepository, IMapper mapper)
    : IQueryHandler<GetRandomQuoteQuery, QuoteVm>
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteVm>> Handle(GetRandomQuoteQuery request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetRandomQuote();

        return Result<QuoteVm>.Success(_mapper.Map<QuoteVm>(quote));
    }
}