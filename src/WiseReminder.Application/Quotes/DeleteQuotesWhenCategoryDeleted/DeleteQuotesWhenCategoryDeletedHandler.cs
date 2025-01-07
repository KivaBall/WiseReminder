namespace WiseReminder.Application.Quotes.DeleteQuotesWhenCategoryDeleted;

public sealed class DeleteQuotesWhenCategoryDeletedHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : INotificationHandler<CategoryDeletedEvent>
{
    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var query = new GetQuotesByCategoryIdQuery(notification.CategoryId);

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