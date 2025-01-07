namespace WiseReminder.Application.Quotes.CreateQuoteByAdmin;

public sealed record CreateQuoteByAdminCommand : ICommand<Guid>
{
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}