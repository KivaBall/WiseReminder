namespace WiseReminder.Application.Quotes.Commands.UpdateQuoteByAdmin;

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

        var hasAccess = author.Value.HasAccess(false);

        if (hasAccess.IsFailed)
        {
            return hasAccess;
        }

        var categoryQuery = new HasCategoryByIdQuery(request.CategoryId);

        var categoryExists = await sender.Send(categoryQuery, cancellationToken);

        if (categoryExists.IsFailed)
        {
            return categoryExists.ToResult();
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return quoteDate.ToResult();
        }

        var result = quote.Value.Update(text, author.Value, request.CategoryId, quoteDate.Value);

        if (result.IsFailed)
        {
            return result;
        }

        await repository.UpdateQuote(quote.Value, cancellationToken);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}