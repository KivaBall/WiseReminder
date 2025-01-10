namespace WiseReminder.Application.Reactions.Queries.GetReactionByIds;

public sealed record GetReactionByIdsQuery(Guid QuoteId, Guid UserId) : IQuery<Reaction>;