namespace WiseReminder.Application.Authors.CreateAuthor;

public sealed class CreateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IAuthorService authorService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IAuthorService _authorService = authorService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var authorName = new AuthorName(request.Name);
        var authorBiography = new AuthorBiography(request.Biography);
        var authorDateOfBirth = new AuthorDateOfBirth(request.DateOfBirth);
        var authorDateOfDeath = new AuthorDateOfDeath(request.DateOfDeath);

        var author = _authorService.CreateAuthor(
            authorName,
            authorBiography,
            authorDateOfBirth,
            authorDateOfDeath);

        _authorRepository.CreateAuthor(author);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}