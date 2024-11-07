using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed class DeleteQuoteCommandHandler(IQuoteRepository quoteRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteQuoteCommand>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        if (quote == null) return Result.Failure(QuoteErrors.QuoteNotFound);

        _quoteRepository.DeleteQuote(quote);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}