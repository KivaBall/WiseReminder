namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed record UpdateQuoteAsUserCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required Guid UserId { get; init; }
}