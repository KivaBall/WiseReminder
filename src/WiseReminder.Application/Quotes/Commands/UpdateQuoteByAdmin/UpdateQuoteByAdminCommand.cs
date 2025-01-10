namespace WiseReminder.Application.Quotes.Commands.UpdateQuoteByAdmin;

public sealed record UpdateQuoteByAdminCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}