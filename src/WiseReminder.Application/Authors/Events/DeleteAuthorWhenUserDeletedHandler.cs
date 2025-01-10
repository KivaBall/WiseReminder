namespace WiseReminder.Application.Authors.Events;

public sealed class DeleteAuthorWhenUserDeletedHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    IMediator mediator)
    : INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(
        UserDeletedEvent notification,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByUserIdQuery(notification.UserId);

        var author = await mediator.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return;
        }

        var authorDeletedEvent = new AuthorDeletedEvent(author.Value.Id);

        await mediator.Publish(authorDeletedEvent, cancellationToken);

        repository.DeleteAuthor(author.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}