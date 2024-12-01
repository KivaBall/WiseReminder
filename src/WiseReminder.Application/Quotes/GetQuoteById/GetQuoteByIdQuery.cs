namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed record GetQuoteByIdQuery(Guid Id) : IQuery<QuoteDto>;