namespace WiseReminder.Application.Quotes.GetRecentAddedQuoteDtos;

public sealed record GetRecentAddedQuoteDtos : IQuery<ICollection<QuoteDto>>
{
    public required int Amount { get; init; }
}