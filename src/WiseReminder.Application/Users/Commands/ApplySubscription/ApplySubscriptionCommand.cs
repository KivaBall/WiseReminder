namespace WiseReminder.Application.Users.Commands.ApplySubscription;

public sealed record ApplySubscriptionCommand : ICommand
{
    public required Guid UserId { get; init; }
    public required string Subscription { get; init; }
}