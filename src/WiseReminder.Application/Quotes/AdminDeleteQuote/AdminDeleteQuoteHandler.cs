namespace WiseReminder.Application.Quotes.AdminDeleteQuote;

public sealed class AdminDeleteQuoteHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<AdminDeleteQuoteCommand>
{
    public async Task<Result> Handle(
        AdminDeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery(request.Id);

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        var authorQuery = new GetAuthorByIdQuery(quote.Value.AuthorId);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }

        quoteRepository.DeleteQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}