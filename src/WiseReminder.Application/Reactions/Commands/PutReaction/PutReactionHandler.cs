namespace WiseReminder.Application.Reactions.Commands.PutReaction;

public sealed class PutReactionHandler(
    ISender sender,
    IReactionRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PutReactionCommand>
{
    public async Task<Result> Handle(PutReactionCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetReactionByIdsQuery(request.QuoteId, request.UserId);

        var reaction = await sender.Send(query, cancellationToken);

        if (reaction.IsFailed)
        {
            var createdReaction = new Reaction(
                request.QuoteId,
                request.UserId,
                new IsLike(request.IsLike));

            repository.CreateReaction(createdReaction);
        }
        else
        {
            var isLike = new IsLike(request.IsLike);

            var result = reaction.Value.ChangeReaction(isLike);

            if (result.IsFailed)
            {
                return result;
            }

            repository.UpdateReaction(reaction.Value);
        }

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}