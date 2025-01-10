namespace WiseReminder.Application.Quotes.Commands.UpdateQuoteByUser;

public sealed record UpdateQuoteByUserCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required Guid UserId { get; init; }
}