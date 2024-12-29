namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record BaseQuoteAsAdminRequest
{
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}