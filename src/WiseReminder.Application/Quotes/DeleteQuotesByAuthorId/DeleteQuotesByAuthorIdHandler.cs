namespace WiseReminder.Application.Quotes.DeleteQuotesByAuthorId;

public sealed class DeleteQuotesByAuthorIdHandler(
    ISender sender,
    IQuoteRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteQuotesByAuthorIdCommand>
{
    public async Task<Result> Handle(
        DeleteQuotesByAuthorIdCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuotesByAuthorIdQuery(request.AuthorId);

        var quotes = await sender.Send(query, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        foreach (var quote in quotes.Value)
        {
            await repository.DeleteQuote(quote);
        }

        return await unitOfWork.SaveChangesAsync();
    }
}