namespace WiseReminder.Application.Quotes.DeleteQuoteByAdmin;

public sealed class DeleteQuoteByAdminHandler(
    IQuoteRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteQuoteByAdminCommand>
{
    public async Task<Result> Handle(
        DeleteQuoteByAdminCommand request,
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
            return author.ToResult();
        }

        if (author.Value.UserId != null)
        {
            return AuthorErrors.AdminCannotModifyUserAuthor;
        }

        await repository.DeleteQuote(quote.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}