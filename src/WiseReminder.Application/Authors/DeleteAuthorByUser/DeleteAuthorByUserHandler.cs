namespace WiseReminder.Application.Authors.DeleteAuthorByUser;

public sealed class DeleteAuthorByUserHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    IMediator mediator)
    : ICommandHandler<DeleteAuthorByUserCommand>
{
    public async Task<Result> Handle(
        DeleteAuthorByUserCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await mediator.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var authorDeletedEvent = new AuthorDeletedEvent(author.Value.Id);

        await mediator.Publish(authorDeletedEvent, cancellationToken);

        repository.DeleteAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}