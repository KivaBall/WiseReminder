namespace WiseReminder.Application.Quotes.UpdateQuoteAsAdmin;

public sealed record UpdateQuoteAsAdminCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}