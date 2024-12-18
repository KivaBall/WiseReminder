namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed class CreateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery { Id = request.AuthorId };

        var authorResult = await sender.Send(authorQuery, cancellationToken);

        if (authorResult.IsFailed)
        {
            return Result.Fail(authorResult.Errors);
        }

        var author = authorResult.Value;

        var categoryQuery = new GetCategoryByIdQuery { Id = request.CategoryId };

        var categoryResult = await sender.Send(categoryQuery, cancellationToken);

        if (categoryResult.IsFailed)
        {
            return Result.Fail(categoryResult.Errors);
        }

        var category = categoryResult.Value;

        var quoteText = new QuoteText(request.Text);

        var quoteDate = Date.Create((short)request.QuoteDate.Year, (byte)request.QuoteDate.Month,
            (byte)request.QuoteDate.Day);

        if (quoteDate.IsFailed)
        {
            return Result.Fail(quoteDate.Errors);
        }

        var quote = Quote.Create(quoteText, author, category, quoteDate.Value);

        if (quote.IsFailed)
        {
            return Result.Fail(quote.Errors);
        }

        quoteRepository.CreateQuote(quote.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok(quote.Value.Id);
    }
}