using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed record GetQuoteByIdQuery(Guid Id) : IQuery<QuoteVm>;