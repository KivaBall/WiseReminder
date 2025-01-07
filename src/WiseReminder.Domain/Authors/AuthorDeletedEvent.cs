namespace WiseReminder.Domain.Authors;

public sealed record AuthorDeletedEvent(Guid AuthorId) : INotification;