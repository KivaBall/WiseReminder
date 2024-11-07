namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record UpdateQuoteRequest(Guid Id, string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate);