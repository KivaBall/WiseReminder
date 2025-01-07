namespace WiseReminder.Application.Quotes.UpdateQuoteByAdmin;

public sealed class UpdateQuoteByAdminHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteByAdminCommand>
{
    public async Task<Result> Handle(
        UpdateQuoteByAdminCommand request,
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
            return AuthorErrors.AdminCannotModifyUserAuthor;
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

        var result = quote.Value.Update(text, author.Value, category.Value.Id, quoteDate.Value);

        if (result.IsFailed)
        {
            return result;
        }

        await repository.UpdateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}