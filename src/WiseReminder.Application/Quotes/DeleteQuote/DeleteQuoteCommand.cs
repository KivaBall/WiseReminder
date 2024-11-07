using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteCommand(Guid Id) : ICommand;