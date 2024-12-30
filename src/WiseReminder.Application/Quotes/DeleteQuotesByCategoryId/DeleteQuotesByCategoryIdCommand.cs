namespace WiseReminder.Application.Quotes.DeleteQuotesByCategoryId;

public sealed record DeleteQuotesByCategoryIdCommand(Guid CategoryId) : ICommand;