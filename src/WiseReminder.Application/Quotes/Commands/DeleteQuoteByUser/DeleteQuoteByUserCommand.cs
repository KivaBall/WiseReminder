namespace WiseReminder.Application.Quotes.Commands.DeleteQuoteByUser;

public sealed record DeleteQuoteByUserCommand(Guid Id, Guid UserId) : ICommand;