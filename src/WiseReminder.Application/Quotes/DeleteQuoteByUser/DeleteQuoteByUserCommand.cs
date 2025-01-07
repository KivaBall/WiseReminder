namespace WiseReminder.Application.Quotes.DeleteQuoteByUser;

public sealed record DeleteQuoteByUserCommand(Guid Id, Guid UserId) : ICommand;