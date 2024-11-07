using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Authors.DeleteAuthor;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteCommand(Guid Id) : ICommand;