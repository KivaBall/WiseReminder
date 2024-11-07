namespace WiseReminder.Application.Quotes;

public sealed record QuoteVm(Guid Id, string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate);