namespace WiseReminder.Application.Quotes.Events;

public sealed class DeleteQuotesWhenAuthorDeletedHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : INotificationHandler<AuthorDeletedEvent>
{
    public async Task Handle(
        AuthorDeletedEvent notification,
        CancellationToken cancellationToken)
    {
        var query = new GetQuotesQuery
        {
            CategoryId = null,
            AuthorId = notification.AuthorId,
            Keywords = null
        };

        var quotes = await sender.Send(query, cancellationToken);

        if (quotes.IsFailed)
        {
            return;
        }

        foreach (var quote in quotes.Value)
        {
            await repository.DeleteQuote(quote, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}