namespace WiseReminder.Application.Quotes.GetRandomQuoteDtos;

public sealed record GetRandomQuoteDtosQuery : IQuery<ICollection<QuoteDto>>
{
    public required int Amount { get; init; }
}