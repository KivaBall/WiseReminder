namespace WiseReminder.Application.Reactions.Commands.DeleteReaction;

public sealed class DeleteReactionHandler(
    ISender sender,
    IReactionRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteReactionCommand>
{
    public async Task<Result> Handle(DeleteReactionCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetReactionByIdsQuery(request.QuoteId, request.UserId);

        var reaction = await sender.Send(query, cancellationToken);

        if (reaction.IsFailed)
        {
            return reaction.ToResult();
        }

        repository.DeleteReaction(reaction.Value);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}