namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record ApplySubscriptionRequest
{
    public required string Subscription { get; init; }
}