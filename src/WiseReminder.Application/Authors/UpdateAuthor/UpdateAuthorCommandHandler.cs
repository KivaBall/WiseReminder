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

        if (author == null)
        {
            return Result.Failure(AuthorErrors.AuthorNotFound);
        }

        var authorName = new AuthorName(request.Name);
        var authorBiography = new AuthorBiography(request.Biography);
        var authorDateOfBirth = new AuthorDateOfBirth(request.DateOfBirth);
        var authorDateOfDeath = new AuthorDateOfDeath(request.DateOfDeath);

        _authorService.UpdateAuthor(author, authorName, authorBiography, authorDateOfBirth, authorDateOfDeath);

        _authorRepository.UpdateAuthor(author);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}