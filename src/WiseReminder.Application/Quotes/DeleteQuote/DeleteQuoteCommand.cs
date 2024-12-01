namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteCommand(Guid Id) : ICommand;