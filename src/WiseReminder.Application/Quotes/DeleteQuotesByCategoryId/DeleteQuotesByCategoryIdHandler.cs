namespace WiseReminder.Application.Quotes.DeleteQuotesByCategoryId;

public sealed class DeleteQuotesByCategoryIdHandler(
    ISender sender,
    IQuoteRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteQuotesByCategoryIdCommand>
{
    public async Task<Result> Handle(
        DeleteQuotesByCategoryIdCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuotesByCategoryIdQuery(request.CategoryId);

        var quotes = await sender.Send(query, cancellationToken);

        if (quotes.IsFailed)
        {
            return quotes.ToResult();
        }

        foreach (var quote in quotes.Value)
        {
            repository.DeleteQuote(quote);
        }

        return await unitOfWork.SaveChangesAsync();
    }
}