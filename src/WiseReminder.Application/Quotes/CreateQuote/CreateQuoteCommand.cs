namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed record CreateQuoteCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}