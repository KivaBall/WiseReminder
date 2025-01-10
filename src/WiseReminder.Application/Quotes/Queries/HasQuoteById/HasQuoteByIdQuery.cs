namespace WiseReminder.Application.Quotes.Queries.HasQuoteById;

public sealed record HasQuoteByIdQuery(Guid Id) : IQuery<bool>;