namespace WiseReminder.Application.Quotes.UpdateQuoteAsUser;

public sealed class UpdateQuoteAsUserCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UpdateQuoteAsUserCommand>
{
    public async Task<Result> Handle(
        UpdateQuoteAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var quoteQuery = new GetQuoteByIdQuery { Id = request.Id };

        var quote = await sender.Send(quoteQuery, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery { Id = request.UserId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (quote.Value.AuthorId == author.Value.Id)
        {
            return Result.Fail(UserErrors.UserIdNotValid);
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

        quote.Value.Update(text, author.Value, category.Value, quoteDate.Value);

        quoteRepository.UpdateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}