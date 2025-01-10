namespace WiseReminder.Domain.Authors;

public sealed record AuthorDetails
{
    public required Author Author { get; init; }
    public required int Quotes { get; init; }
    public required Date? MinQuoteDate { get; init; }
    public required Date? MaxQuoteDate { get; init; }
}