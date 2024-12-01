namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed class UpdateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    ICategoryRepository categoryRepository,
    IAuthorRepository authorRepository,
    IQuoteService quoteService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateQuoteCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetQuoteById(request.Id);

        if (quote == null)
        {
            return Result.Failure(QuoteErrors.QuoteNotFound);
        }

        var quoteText = new QuoteText(request.Text);
        var quoteDate = new QuoteDate(request.QuoteDate);

        var author = await _authorRepository.GetAuthorById(request.AuthorId);

        if (author == null)
        {
            return Result.Failure(AuthorErrors.AuthorNotFound);
        }

        var category = await _categoryRepository.GetCategoryById(request.CategoryId);

        if (category == null)
        {
            return Result.Failure(CategoryErrors.CategoryNotFound);
        }

        _quoteService.UpdateQuote(quote, quoteText, author.Id, author, category.Id, category, quoteDate);
        _quoteRepository.UpdateQuote(quote);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}