namespace WiseReminder.Application.Reactions.Commands.PutReaction;

public sealed record PutReactionCommand(Guid QuoteId, Guid UserId, bool IsLike) : ICommand;