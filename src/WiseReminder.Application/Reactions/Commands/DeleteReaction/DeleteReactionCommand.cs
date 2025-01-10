namespace WiseReminder.Application.Reactions.Commands.DeleteReaction;

public sealed record DeleteReactionCommand(Guid QuoteId, Guid UserId) : ICommand;