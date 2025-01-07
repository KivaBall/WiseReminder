namespace WiseReminder.Application.Quotes.DeleteQuotesWhenAuthorDeleted;

public sealed class DeleteQuotesWhenAuthorDeletedHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : INotificationHandler<AuthorDeletedEvent>
{
    public async Task Handle(AuthorDeletedEvent notification, CancellationToken cancellationToken)
    {
        var query = new GetQuotesByAuthorIdQuery(notification.AuthorId);

        var quotes = await sender.Send(query, cancellationToken);

        if (quotes.IsFailed)
        {
            return;
        }

        foreach (var quote in quotes.Value)
        {
            await repository.DeleteQuote(quote);
        }

        await unitOfWork.SaveChangesAsync();
    }
}