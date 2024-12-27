namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed class CreateQuoteAsUserCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteAsUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery { Id = request.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        if (author.Value.UserId == null)
        {
            return Result.Fail(AuthorErrors.AuthorNotExistsForUser);
        }

        if (author.Value.UserId != request.UserId)
        {
            return Result.Fail(UserErrors.UserIdNotValid);
        }
        
        var categoryQuery = new GetCategoryByIdQuery { Id = request.CategoryId };

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return Result.Fail(category.Errors);
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return Result.Fail(quoteDate.Errors);
        }

        var quote = Quote.Create(text, author.Value, category.Value, quoteDate.Value);

        if (quote.IsFailed)
        {
            return Result.Fail(quote.Errors);
        }

        quoteRepository.CreateQuote(quote.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok(quote.Value.Id);
    }
}