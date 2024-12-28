namespace WiseReminder.Application.Quotes.CreateQuoteAsAdmin;

public sealed class CreateQuoteAsAdminCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteAsAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery { Id = request.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        var categoryQuery = new GetCategoryByIdQuery { Id = request.CategoryId };

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

        var quote = Quote.Create(text, author.Value, category.Value, quoteDate.Value);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        quoteRepository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => quote.Value.Id);
    }
}