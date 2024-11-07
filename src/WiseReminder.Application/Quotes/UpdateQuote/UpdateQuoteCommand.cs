using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Authors.UpdateAuthor;

namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed record UpdateQuoteCommand(Guid Id, string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate) : ICommand;