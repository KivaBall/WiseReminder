namespace WiseReminder.Application.Quotes.UserDeleteQuote;

public sealed class UserDeleteQuoteHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<UserDeleteQuoteCommand>
{
    public async Task<Result> Handle(
        UserDeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByUserIdQuery(request.UserId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        if (quote.Value.AuthorId != author.Value.Id)
        {
            return Result.Fail(UserErrors.UserIdNotValid);
        }

        quoteRepository.DeleteQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}