namespace WiseReminder.Application.Reactions.Queries.GetReactionByIds;

public sealed class GetReactionByIdsHandler(
    IReactionRepository repository,
    ISender sender)
    : IQueryHandler<GetReactionByIdsQuery, Reaction>
{
    public async Task<Result<Reaction>> Handle(GetReactionByIdsQuery request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new HasAuthorByUserIdQuery(request.UserId);

        var authorExists = await sender.Send(authorQuery, cancellationToken);

        if (authorExists.IsFailed)
        {
            return authorExists.ToResult();
        }

        var quoteQuery = new HasQuoteByIdQuery(request.QuoteId);

        var quoteExists = await sender.Send(quoteQuery, cancellationToken);

        if (quoteExists.IsFailed)
        {
            return quoteExists.ToResult();
        }

        var reaction = await repository.GetReactionByIds(
            request.QuoteId,
            request.UserId,
            cancellationToken);

        if (reaction == null)
        {
            return ReactionErrors.ReactionNotFound;
        }

        return reaction;
    }
}