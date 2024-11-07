using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed class GetQuoteByIdQueryHandler(IQuoteRepository quoteRepository, IMapper mapper)
    : IQueryHandler<GetQuoteByIdQuery, QuoteVm>
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public async Task<Result<QuoteVm>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        if (quote == null) return Result<QuoteVm>.Failure<QuoteVm>(null, QuoteErrors.QuoteNotFound);

        return Result<QuoteVm>.Success(_mapper.Map<QuoteVm>(quote));
    }
}