namespace WiseReminder.Application.Quotes.GetRandomQuoteDtos;

public sealed record GetRandomQuoteDtosQuery(int Amount) : IQuery<ICollection<QuoteDto>>;