namespace WiseReminder.Domain.Reactions;

public sealed class Reaction : Entity<Reaction>
{
    public Reaction(Guid quoteId, Guid userId, IsLike isLike)
    {
        QuoteId = quoteId;
        UserId = userId;
        IsLike = isLike;
    }

    public Guid QuoteId { get; private set; }
    public Guid UserId { get; private set; }
    public IsLike IsLike { get; private set; }

    public Result ChangeReaction(IsLike isLike)
    {
        if (IsLike == isLike)
        {
            return ReactionErrors.TheSameReaction;
        }

        IsLike = isLike;

        return Result.Ok();
    }
}