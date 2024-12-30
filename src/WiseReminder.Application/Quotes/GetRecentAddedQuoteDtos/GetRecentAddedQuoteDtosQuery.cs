namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDtos;

public sealed record GetRecentAddedQuoteDtosQuery(int Amount) : IQuery<ICollection<QuoteDto>>;