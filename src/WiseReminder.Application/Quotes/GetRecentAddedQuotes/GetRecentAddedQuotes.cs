namespace WiseReminder.Application.Quotes.GetRecentAddedQuotes;

public sealed record GetRecentAddedQuotes : IQuery<ICollection<QuoteDto>>
{
    public required int Amount { get; init; }
}