namespace WiseReminder.Application.Quotes.GetRecentAddedQuotes;

public sealed record GetRecentAddedQuotes(int Amount) : IQuery<ICollection<QuoteDto>>;