namespace WiseReminder.Application.Quotes.CreateQuoteAsUser;

public sealed record CreateQuoteAsUserCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required Guid UserId { get; init; }
}