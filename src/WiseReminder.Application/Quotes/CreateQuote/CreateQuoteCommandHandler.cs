using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed class CreateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    ICategoryRepository categoryRepository,
    IAuthorRepository authorRepository,
    IQuoteService quoteService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateQuoteCommand>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var quoteText = new QuoteText(request.Text);
        var quoteDate = new QuoteDate(request.QuoteDate);

        var author = await _authorRepository.GetAuthorById(request.AuthorId);

        if (author == null) return Result.Failure(AuthorErrors.AuthorNotFound);

        var category = await _categoryRepository.GetCategoryById(request.CategoryId);

        if (category == null) return Result.Failure(CategoryErrors.CategoryNotFound);

        var quote = _quoteService.CreateQuote(quoteText, author.Id, author, category.Id, category, quoteDate);

        _quoteRepository.CreateQuote(quote);

        return await _unitOfWork.SaveChangesAsync() ? Result.Success() : Result.Failure(Error.Database);
    }
}