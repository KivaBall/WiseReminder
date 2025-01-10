namespace WiseReminder.Application.Quotes.Events;

public sealed class DeleteQuotesWhenCategoryDeletedHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : INotificationHandler<CategoryDeletedEvent>
{
    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var query = new GetQuotesQuery
        {
            CategoryId = notification.CategoryId,
            AuthorId = null,
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