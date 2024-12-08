namespace WiseReminder.Application.Quotes.GetRandomQuotes;

public sealed record GetRandomQuotesQuery(int Amount) : IQuery<ICollection<QuoteDto>>;