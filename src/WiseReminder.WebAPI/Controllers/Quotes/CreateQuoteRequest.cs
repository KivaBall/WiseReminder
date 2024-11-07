namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record CreateQuoteRequest(string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate);