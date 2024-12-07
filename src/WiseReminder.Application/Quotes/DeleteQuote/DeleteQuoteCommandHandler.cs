namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed class DeleteQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteCommand>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        DeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetQuoteByIdQuery(request.Id), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error);
        }

        var quote = result.Entity!;

        _quoteRepository.DeleteQuote(quote);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}