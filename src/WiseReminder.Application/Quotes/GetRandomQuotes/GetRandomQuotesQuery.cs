namespace WiseReminder.Application.Quotes.GetRandomQuotes;

public sealed record GetRandomQuotesQuery : IQuery<ICollection<QuoteDto>>
{
    public int Amount { get; init; }
}