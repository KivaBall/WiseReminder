namespace WiseReminder.Application.Quotes.Commands.CreateQuoteByAdmin;

public sealed class CreateQuoteByAdminHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteByAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteByAdminCommand request,
        CancellationToken cancellationToken)
    {
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

        var quote = Quote.CreateByAdmin(text, quoteDate.Value, author.Value, request.CategoryId);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        repository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync(quote.Value.Id, cancellationToken);
    }
}