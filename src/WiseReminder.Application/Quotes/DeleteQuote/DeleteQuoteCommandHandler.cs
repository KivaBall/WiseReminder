namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed class DeleteQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery { Id = request.Id };

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return Result.Fail(quote.Errors);
        }

        quoteRepository.DeleteQuote(quote.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}