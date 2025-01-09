namespace WiseReminder.Domain.Reactions;

public interface IReactionRepository
{
    void CreateReaction(Reaction reaction);

    void UpdateReaction(Reaction reaction);

    void DeleteReaction(Reaction reaction);

    Task<Reaction?> GetReactionByIds(Guid quoteId, Guid userId,
        CancellationToken cancellationToken);
}