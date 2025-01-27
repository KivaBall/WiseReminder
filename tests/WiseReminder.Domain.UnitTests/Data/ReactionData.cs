namespace WiseReminder.Domain.UnitTests.Data;

public static class ReactionData
{
    public static Guid QuoteId => Guid.Empty;

    public static Guid UserId => Guid.Empty;

    public static IsLike IsLike => new IsLike(true);

    public static IsLike NewIsLike => new IsLike(false);

    public static Reaction Reaction => new Reaction(QuoteId, UserId, IsLike);
}