using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.GetRandomQuote;

public sealed record GetRandomQuoteQuery : IQuery<QuoteVm>;