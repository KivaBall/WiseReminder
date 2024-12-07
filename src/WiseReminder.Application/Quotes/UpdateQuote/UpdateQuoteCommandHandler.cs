namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed class UpdateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IQuoteService quoteService,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteCommand>
{
    private readonly IQuoteRepository _quoteRepository = quoteRepository;
    private readonly IQuoteService _quoteService = quoteService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result> Handle(
        UpdateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var resultQuote = await _sender.Send(new GetQuoteByIdQuery(request.Id), cancellationToken);

        if (!resultQuote.IsSuccess)
        {
            return Result.Failure(resultQuote.Error);
        }

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

        var quote = resultQuote.Entity!;
        var author = resultAuthor.Entity!;
        var category = resultCategory.Entity!;

        var quoteText = new QuoteText(request.Text);
        var quoteDate = new QuoteDate(request.QuoteDate);

        _quoteService.UpdateQuote(
            quote,
            quoteText,
            author.Id,
            author,
            category.Id,
            category,
            quoteDate);

        _quoteRepository.UpdateQuote(quote);

        return await _unitOfWork.SaveChangesAsync()
            ? Result.Success()
            : Result.Failure(Error.Database);
    }
}