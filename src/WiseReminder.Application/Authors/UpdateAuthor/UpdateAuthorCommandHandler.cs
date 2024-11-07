using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Authors.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IAuthorService authorService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IAuthorService _authorService = authorService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        if (author == null) return Result.Failure(AuthorErrors.AuthorNotFound);

        _authorService.UpdateAuthor(author, new AuthorName(request.Name),
            new AuthorBiography(request.Biography), new AuthorDateOfBirth(request.DateOfBirth),
            new AuthorDateOfDeath(request.DateOfDeath));
        _authorRepository.UpdateAuthor(author);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}