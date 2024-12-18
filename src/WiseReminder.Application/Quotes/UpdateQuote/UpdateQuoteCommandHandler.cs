namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed class UpdateQuoteCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteCommand>
{
    public async Task<Result> Handle(
        UpdateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery { Id = request.Id };

        var quoteResult = await sender.Send(quoteQuery, cancellationToken);

        if (quoteResult.IsFailed)
        {
            return Result.Fail(quoteResult.Errors);
        }

        var quote = quoteResult.Value;

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

        quote.Update(quoteText, author, category, quoteDate.Value);

        quoteRepository.UpdateQuote(quote);

        var isSaved = await unitOfWork.SaveChangesAsync();

        if (isSaved.IsFailed)
        {
            return Result.Fail(isSaved.Errors);
        }

        return Result.Ok();
    }
}