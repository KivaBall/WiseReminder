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

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var quote = result.Value;

        quoteRepository.DeleteQuote(quote);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok();
    }
}