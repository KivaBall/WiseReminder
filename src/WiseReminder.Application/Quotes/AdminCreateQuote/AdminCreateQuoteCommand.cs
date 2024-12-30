namespace WiseReminder.Application.Quotes.AdminCreateQuote;

public sealed record AdminCreateQuoteCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}