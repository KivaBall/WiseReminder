namespace WiseReminder.Application.Quotes.UpdateQuoteAsAdmin;

public sealed class UpdateQuoteAsAdminCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteAsAdminCommand>
{
    public async Task<Result> Handle(
        UpdateQuoteAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery { Id = request.Id };

        var quote = await sender.Send(quoteQuery, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

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
            return Result.Fail(quoteDate.Errors);
        }

        quote.Value.Update(text, author.Value, category.Value, quoteDate.Value);

        quoteRepository.UpdateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}