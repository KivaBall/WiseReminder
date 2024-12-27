namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed class DeleteQuoteAsAdminCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteAsAdminCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetQuoteByIdQuery { Id = request.Id };

        var quote = await sender.Send(query, cancellationToken);

        if (quote.IsFailed)
        {
            return Result.Fail(quote.Errors);
        }

        var authorQuery = new GetAuthorByIdQuery{Id = quote.Value.AuthorId};
        
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

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}