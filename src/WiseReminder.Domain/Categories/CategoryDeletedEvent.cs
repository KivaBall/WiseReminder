namespace WiseReminder.Domain.Categories;

public sealed record CategoryDeletedEvent(Guid CategoryId) : INotification;