using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Authors.CreateAuthor;

namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed record CreateQuoteCommand(string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate) : ICommand;