namespace WiseReminder.Infrastructure.Repositories;

public sealed class ReactionRepository(
    AppDbContext context)
    : IReactionRepository
{
    public void CreateReaction(Reaction reaction)
    {
        context.Reactions.Add(reaction);
    }

    public void UpdateReaction(Reaction reaction)
    {
        context.Reactions.Update(reaction);
    }

    public void DeleteReaction(Reaction reaction)
    {
        reaction.Delete();

        context.Reactions.Update(reaction);
    }

    public async Task<Reaction?> GetReactionByIds(Guid quoteId, Guid userId,
        CancellationToken cancellationToken)
    {
        return await context.Reactions
            .Where(r => r.QuoteId == quoteId && r.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}