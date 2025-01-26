namespace WiseReminder.Application.Authors.Commands.DeleteAuthorByAdmin;

public sealed class DeleteAuthorByAdminHandler(
    IAuthorRepository repository,
    IUnitOfWork unitOfWork,
    IMediator mediator)
    : ICommandHandler<DeleteAuthorByAdminCommand>
{
    public async Task<Result> Handle(
        DeleteAuthorByAdminCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(request.Id);

        var author = await mediator.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var hasAccess = author.Value.HasAccess(false);

        if (hasAccess.IsFailed)
        {
            return hasAccess;
        }

        repository.DeleteAuthor(author.Value);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return result;
        }

        var authorDeletedEvent = new AuthorDeletedEvent(request.Id);

        await mediator.Publish(authorDeletedEvent, cancellationToken);

        return result;
    }
}