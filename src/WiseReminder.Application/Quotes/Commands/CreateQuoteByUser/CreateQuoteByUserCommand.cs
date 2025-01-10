namespace WiseReminder.Application.Quotes.Commands.CreateQuoteByUser;

public sealed record CreateQuoteByUserCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required Guid UserId { get; init; }
}