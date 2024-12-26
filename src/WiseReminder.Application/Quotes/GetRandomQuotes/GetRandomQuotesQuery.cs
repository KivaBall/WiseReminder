namespace WiseReminder.Application.Quotes.GetRandomQuotes;

public sealed record GetRandomQuotesQuery : IQuery<ICollection<QuoteDto>>
{
    public required int Amount { get; init; }
}