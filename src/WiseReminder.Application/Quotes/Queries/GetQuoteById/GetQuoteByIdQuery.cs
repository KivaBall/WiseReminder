namespace WiseReminder.Application.Quotes.Queries.GetQuoteById;

public sealed record GetQuoteByIdQuery(Guid Id) : IQuery<Quote>;