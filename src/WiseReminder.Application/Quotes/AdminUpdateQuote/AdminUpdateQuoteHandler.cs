namespace WiseReminder.Application.Quotes.AdminUpdateQuote;

public sealed class AdminUpdateQuoteHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<AdminUpdateQuoteCommand>
{
    public async Task<Result> Handle(
        AdminUpdateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(quoteQuery, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByIdQuery(request.AuthorId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        var categoryQuery = new GetCategoryByIdQuery(request.CategoryId);

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return Result.Fail(quoteDate.Errors);
        }

        quote.Value.Update(text, author.Value, category.Value, quoteDate.Value);

        quoteRepository.UpdateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}