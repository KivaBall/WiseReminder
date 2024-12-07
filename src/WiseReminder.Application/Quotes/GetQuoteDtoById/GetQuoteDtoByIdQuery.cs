namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed record GetQuoteDtoByIdQuery(Guid Id) : IQuery<QuoteDto>;