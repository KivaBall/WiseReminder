namespace WiseReminder.WebAPI.Controllers.Quotes;

//TODO: Replace with inherited records
public sealed record CreateQuoteRequest(string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate);