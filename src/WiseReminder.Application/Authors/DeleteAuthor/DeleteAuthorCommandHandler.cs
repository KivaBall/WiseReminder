namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteAuthorCommand>
{
    public async Task<Result> Handle(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.Id };

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        var author = result.Value;

        await authorRepository.DeleteAuthor(author);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok();
    }
}