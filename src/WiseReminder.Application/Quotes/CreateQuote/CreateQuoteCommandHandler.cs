namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed class CreateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IQuoteService quoteService,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteCommand>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        CreateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var resultAuthor = await _sender.Send(new GetAuthorByIdQuery(request.AuthorId), cancellationToken);

        if (!resultAuthor.IsSuccess)
        {
            return Result.Failure(resultAuthor.Error);
        }

        var resultCategory = await _sender.Send(new GetCategoryByIdQuery(request.CategoryId), cancellationToken);

        if (!resultCategory.IsSuccess)
        {
            return Result.Failure(resultCategory.Error);
        }

        var author = resultAuthor.Entity!;
        var category = resultCategory.Entity!;

        var quoteText = new QuoteText(request.Text);
        var quoteDate = new QuoteDate(request.QuoteDate);

        var quote = _quoteService.CreateQuote(
            quoteText,
            author.Id,
            author,
            category.Id,
            category,
            quoteDate);

        _quoteRepository.CreateQuote(quote);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}