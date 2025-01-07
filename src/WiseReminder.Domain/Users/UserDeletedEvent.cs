namespace WiseReminder.Domain.Users;

public sealed record UserDeletedEvent(Guid UserId) : INotification;