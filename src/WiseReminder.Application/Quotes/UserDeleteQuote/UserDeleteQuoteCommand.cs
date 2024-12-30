namespace WiseReminder.Application.Quotes.UserDeleteQuote;

public sealed record UserDeleteQuoteCommand(Guid Id, Guid UserId) : ICommand;