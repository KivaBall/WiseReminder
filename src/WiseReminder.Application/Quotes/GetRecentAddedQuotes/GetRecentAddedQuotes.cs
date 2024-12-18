namespace WiseReminder.Application.Quotes.GetRecentAddedQuotes;

public sealed record GetRecentAddedQuotes : IQuery<ICollection<QuoteDto>>
{
    public int Amount { get; init; }
}