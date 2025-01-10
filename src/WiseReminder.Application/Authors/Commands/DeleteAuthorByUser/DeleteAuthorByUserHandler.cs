namespace WiseReminder.Application.Authors.Commands.DeleteAuthorByUser;

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
        var query = new GetAuthorByUserIdQuery(request.UserId);

        var author = await mediator.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        repository.DeleteAuthor(author.Value);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result.IsSuccess)
        {
            var authorDeletedEvent = new AuthorDeletedEvent(author.Value.Id);

            await mediator.Publish(authorDeletedEvent, cancellationToken);
        }

        return result;
    }
}