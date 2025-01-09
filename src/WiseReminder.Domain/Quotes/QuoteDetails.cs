namespace WiseReminder.Domain.Quotes;

public sealed record QuoteDetails
{
    public required Quote Quote { get; init; }
    public required int Likes { get; init; }
    public required int Dislikes { get; init; }
}