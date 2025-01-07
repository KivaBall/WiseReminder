namespace WiseReminder.Application.Authors.DeleteAuthorByAdmin;

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
        var authorQuery = new GetAuthorByIdQuery(request.Id);

        var author = await mediator.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return AuthorErrors.AdminCannotModifyUserAuthor;
        }

        var authorDeletedEvent = new AuthorDeletedEvent(request.Id);

        await mediator.Publish(authorDeletedEvent, cancellationToken);

        repository.DeleteAuthor(author.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}