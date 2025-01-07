namespace WiseReminder.Application.Quotes.CreateQuoteByAdmin;

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
            return quoteDate.ToResult();
        }

        var quote = Quote.CreateByAdmin(text, quoteDate.Value, author.Value, category.Value.Id);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        repository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => quote.Value.Id);
    }
}