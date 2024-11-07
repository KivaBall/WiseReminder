using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed record CreateQuoteCommand(string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate) : ICommand;