using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        if (author == null) return Result.Failure(AuthorErrors.AuthorNotFound);

        _authorRepository.DeleteAuthor(author);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}