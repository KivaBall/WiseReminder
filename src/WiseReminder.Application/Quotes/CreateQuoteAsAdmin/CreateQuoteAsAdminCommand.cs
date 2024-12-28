namespace WiseReminder.Application.Quotes.CreateQuoteAsAdmin;

public sealed record CreateQuoteAsAdminCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}