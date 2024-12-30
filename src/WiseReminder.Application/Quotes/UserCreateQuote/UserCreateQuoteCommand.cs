namespace WiseReminder.Application.Quotes.UserCreateQuote;

public sealed record UserCreateQuoteCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required Guid UserId { get; init; }
}