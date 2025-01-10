namespace WiseReminder.Application.Quotes.Commands.DeleteQuoteByAdmin;

public sealed record DeleteQuoteByAdminCommand(Guid Id) : ICommand;