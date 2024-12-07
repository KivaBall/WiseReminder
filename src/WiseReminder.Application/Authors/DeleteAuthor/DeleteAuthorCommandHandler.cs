namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAuthorByIdQuery(request.Id), cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error);
        }

        var author = result.Entity!;

        await _authorRepository.DeleteAuthor(author);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}