namespace WiseReminder.Application.Quotes.Commands.DeleteQuoteByAdmin;

public sealed class DeleteQuoteByAdminHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteByAdminCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteByAdminCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(quoteQuery, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByIdQuery(quote.Value.AuthorId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var hasAccess = author.Value.HasAccess(false);

        if (hasAccess.IsFailed)
        {
            return hasAccess;
        }

        await repository.DeleteQuote(quote.Value, cancellationToken);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}